using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class PlayerMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const int Right = 1;
        private const int Left = -1;

        private EcsFilter _playerFilter;
        private EcsPool<PlayerInputComponent> _playerInputComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<DestinationComponent> _destinationComponentPool;
        private EcsPool<IsPlayerMoveComponent> _isPlayerMoveComponentPool;
        private EcsPool<IsOnGroundComponent> _isOnGroundComponentPool;
        private EcsPool<PlatformSideComponent> _platformSideComponentPool;
        private PlatformSide _platformSide;
        private ITimeService _timeService;
        private float _distance;
        private int _direction;
        private float _startTime;
        private Vector3 _newPosition;
        private Vector3 _startPosition;
        private EcsPool<SpeedVectorComponent> _speedVectorComponentPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _playerFilter = world.Filter<IsPlayerComponent>().Inc<TransformComponent>().End();
            _playerInputComponentPool = world.GetPool<PlayerInputComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _destinationComponentPool = world.GetPool<DestinationComponent>();
            _isPlayerMoveComponentPool = world.GetPool<IsPlayerMoveComponent>();
            _platformSideComponentPool = world.GetPool<PlatformSideComponent>();
            _speedVectorComponentPool = world.GetPool<SpeedVectorComponent>();
            _isOnGroundComponentPool = world.GetPool<IsOnGroundComponent>();
            _timeService = Service<ITimeService>.Get();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerFilter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                ref PlayerInputComponent playerInputComponent = ref _playerInputComponentPool.Get(entity);
                ref DestinationComponent destinationComponent = ref _destinationComponentPool.Get(entity);
                ref SpeedVectorComponent speedVectorComponent = ref _speedVectorComponentPool.Get(entity);
                ref PlatformSideComponent platformSideComponent = ref _platformSideComponentPool.Get(entity);


                if (playerInputComponent.Horizontal != 0 && !_isPlayerMoveComponentPool.Has(entity) &&
                    _isOnGroundComponentPool.Has(entity))
                {
                    //Add jump Sound
                    ref IsPlaySoundComponent isSoundFromTriggerComponent =
                        ref systems.GetWorld().GetPool<IsPlaySoundComponent>().Add(entity);
                    isSoundFromTriggerComponent.SoundType = SoundsEnumType.Jump;
                    //

                    _isPlayerMoveComponentPool.Add(entity);
                    InitializeStartMoving(ref transformComponent, ref playerInputComponent, ref destinationComponent,
                        entity);
                }


                if (_isPlayerMoveComponentPool.Has(entity))
                {
                    SmoothMoving(entity, ref speedVectorComponent, ref transformComponent,
                        ref platformSideComponent, ref destinationComponent);
                }
            }
        }

        private void InitializeStartMoving(ref TransformComponent transformComponent,
            ref PlayerInputComponent inputComponent, ref DestinationComponent destinationComponent, int entity)
        {
            ref IsPlayerMoveComponent isPlayerMoveComponent = ref _isPlayerMoveComponentPool.Get(entity);
            isPlayerMoveComponent.Direction = (int)inputComponent.Horizontal;
            isPlayerMoveComponent.StartMovePosition = transformComponent.Value.position;

            _direction = (int)inputComponent.Horizontal;
            _startPosition = transformComponent.Value.position;
            _newPosition =
                new Vector3(
                    _startPosition.x + destinationComponent.Value.x * _direction, _startPosition.y, 0);
            _startTime = _timeService.InGameTime;

            _distance = Vector3.Distance(_startPosition, _newPosition);
        }

        private void SmoothMoving(int entity, ref SpeedVectorComponent speedVectorComponent,
            ref TransformComponent transformComponent, ref PlatformSideComponent platformSideComponent,
            ref DestinationComponent destinationComponent)
        {
            switch (_direction)
            {
                case 0:
                    return;
                case Right when platformSideComponent.PlatformSide == PlatformSide.Right:
                case Left when platformSideComponent.PlatformSide == PlatformSide.Left:
                    _isPlayerMoveComponentPool.Del(entity);
                    return;
            }

            float distCovered = (_timeService.InGameTime - _startTime) * speedVectorComponent.Value.x;

            float fractionOfJourney = distCovered / _distance;

            Vector3 position = Extensions.GetPoint(_startPosition, new Vector3(_startPosition.x, destinationComponent.Value.y, 0),
                new Vector3(_newPosition.x, destinationComponent.Value.y, 0), _newPosition, fractionOfJourney);

            transformComponent.Value.position = position;

            if (_newPosition == transformComponent.Value.position)
            {
                _isPlayerMoveComponentPool.Del(entity);

                platformSideComponent.PlatformSide = _direction == Right ? PlatformSide.Right : PlatformSide.Left;

                if (_newPosition.x == 0)
                    platformSideComponent.PlatformSide = PlatformSide.Center;

                _direction = 0;
            }
        }
        
    }
}