using System.Collections.Generic;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class TreadmillBuildSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<CreateGameObjectComponent> _createGameObjectComponentPool;
        private EcsPool<TreadmillComponent> _treadmillComponentPool;
        private EcsPool<IsPlatformComponent> _isPlatformComponentPool;
        private IPoolService _poolService;
        private float _platformLenght;
        private EcsPool<IsMoveComponent> _isMoveComponentPool;
        private EcsPool<IsLastPlatformComponent> _isLastPlatformPool;
        private EcsPool<IsLampSpawnComponent> _isLampSpawnComponentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsTreadmillComponent>().Inc<CreateGameObjectComponent>().End();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _createGameObjectComponentPool = _world.GetPool<CreateGameObjectComponent>();
            _treadmillComponentPool = _world.GetPool<TreadmillComponent>();
            _isPlatformComponentPool = _world.GetPool<IsPlatformComponent>();
            _isMoveComponentPool = _world.GetPool<IsMoveComponent>();
            _isLastPlatformPool = _world.GetPool<IsLastPlatformComponent>();
            _isLampSpawnComponentPool = _world.GetPool<IsLampSpawnComponent>();
            _poolService = Service<IPoolService>.Get();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                GameObject treadmill = new("Trek");

                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                transformComponent.Value = treadmill.transform;

                ref TreadmillComponent treadmillComponent = ref _treadmillComponentPool.Get(entity);
                
                InitializeTrack(ref treadmillComponent, ref transformComponent);

                _createGameObjectComponentPool.Del(entity);
            }
        }

        private Vector3 GetPlatformPositionOnTrack(int index, ref TreadmillComponent treadmillComponent)
        {
            return new(0, 0, -(treadmillComponent.StartPlatformCount - treadmillComponent.PlatformsBeforePlayer -
                               index) * _platformLenght);
        }

        private void InitializeTrack(ref TreadmillComponent treadmillComponent,
            ref TransformComponent transformComponent)
        {
            for (int i = 0; i < treadmillComponent.StartPlatformCount; i++)
            {
                PlatformView newPlatform = _poolService.Get(treadmillComponent.UsingPlatform).GetComponent<PlatformView>();

                if (_platformLenght == 0)
                {
                    _platformLenght = newPlatform.GetPlatformLenght();
                    treadmillComponent.PlatformLength = _platformLenght;
                }

                Transform platformTransform = newPlatform.transform;
                platformTransform.position = GetPlatformPositionOnTrack(i, ref treadmillComponent);
                platformTransform.parent = transformComponent.Value;

                int platformEntity = _world.NewEntity();
                _isPlatformComponentPool.Add(platformEntity);
                _transformComponentPool.Add(platformEntity);
                _isMoveComponentPool.Add(platformEntity);
                _isLampSpawnComponentPool.Add(platformEntity);
                
                ref TransformComponent platformTransformComponent = ref _transformComponentPool.Get(platformEntity);
                platformTransformComponent.Value = newPlatform.transform;
                
                ref IsPlatformComponent isPlatformComponent = ref _isPlatformComponentPool.Get(platformEntity);
                isPlatformComponent.PillarLamps = new List<GameObject>();
                isPlatformComponent.WallLamps = new List<GameObject>();
                isPlatformComponent.PickableObjects = new List<GameObject>();

                if (i == 0)
                    treadmillComponent.ReturnToPoolPoint = newPlatform.StartPointWorldPosition;
                
                if (i == treadmillComponent.StartPlatformCount - 1)
                {
                    treadmillComponent.SpawnPlatformPoint = newPlatform.EndPointWorldPosition;
                    _isLastPlatformPool.Add(platformEntity);
                }

                treadmillComponent.Platforms.Enqueue(newPlatform);
            }
        }
    }
}