using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class PlayerBounceSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _playerFilter;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<SpeedVectorComponent> _speedVectorComponentPool;
        private EcsPool<IsPlayerBounceComponent> _isPlayerBounceComponentPool;
        private ITimeService _timeService;
        private float _distance;
        private float _startTime;
        private Vector3 _newPosition;
        private Vector3 _startPosition;
        private bool _bounce;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _playerFilter = world.Filter<IsPlayerComponent>().Inc<TransformComponent>().Inc<IsPlayerBounceComponent>().End();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _speedVectorComponentPool = world.GetPool<SpeedVectorComponent>();
            _isPlayerBounceComponentPool = world.GetPool<IsPlayerBounceComponent>();
            _timeService = Service<ITimeService>.Get();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerFilter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                ref SpeedVectorComponent speedVectorComponent = ref _speedVectorComponentPool.Get(entity);
                ref IsPlayerBounceComponent isPlayerBounceComponent = ref _isPlayerBounceComponentPool.Get(entity);

                if (_isPlayerBounceComponentPool.Has(entity) && !_bounce)
                {
                    InitializeStartBounce(ref transformComponent, ref isPlayerBounceComponent);
                }


                if (_isPlayerBounceComponentPool.Has(entity) && _bounce)
                {
                    SmoothMoving(entity, ref speedVectorComponent, ref transformComponent, ref isPlayerBounceComponent);
                }
            }
        }

        private void InitializeStartBounce(ref TransformComponent transformComponent,
            ref IsPlayerBounceComponent isPlayerBounceComponent)
        {
            _startPosition = transformComponent.Value.position;
            _newPosition = isPlayerBounceComponent.ReturnPosition;
            _startTime = _timeService.InGameTime;
            _distance = Vector3.Distance(_startPosition, _newPosition);
            _bounce = true;
        }
        private void SmoothMoving(int entity, ref SpeedVectorComponent speedVectorComponent,
            ref TransformComponent transformComponent, 
            ref IsPlayerBounceComponent isPlayerBounceComponent)
        {
            float distCovered = (_timeService.InGameTime - _startTime) * speedVectorComponent.Value.x;

            float fractionOfJourney = distCovered / _distance;

            Vector3 position = Extensions.GetPoint(_startPosition, new Vector3(isPlayerBounceComponent.ReturnPosition.x, 1, 0),
                new Vector3(isPlayerBounceComponent.ReturnPosition.x, 1, 0), _newPosition, fractionOfJourney);

            transformComponent.Value.position = position;

            if (_newPosition == transformComponent.Value.position)
            {
                _isPlayerBounceComponentPool.Del(entity);
                _bounce = false;
            }
        }
        
       
    }
}