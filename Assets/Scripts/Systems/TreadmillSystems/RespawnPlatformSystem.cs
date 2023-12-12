using System;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using StaticData;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class RespawnPlatformSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _platformFilter;
        private EcsFilter _treadmillFilter;
        private EcsFilter _lastPlatformFilter;
        
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<TreadmillComponent> _treadmillComponentPool;
        private EcsPool<IsLastPlatformComponent> _isLastPlatformComponentPool;
        private EcsPool<IsObjectSpawnComponent> _isObjectSpawnComponentPool;
        private EcsPool<IsLampSpawnComponent> _isLampSpawnComponentPool;
        private EcsPool<IsMoveComponent> _isMoveComponentPool;
        
        private IPoolService _poolService;
        private IPatternService _patternService;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _treadmillFilter = _world.Filter<IsTreadmillComponent>().End();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _treadmillComponentPool = _world.GetPool<TreadmillComponent>();
            _isMoveComponentPool = _world.GetPool<IsMoveComponent>();
            _isLastPlatformComponentPool = _world.GetPool<IsLastPlatformComponent>();
            _isObjectSpawnComponentPool = _world.GetPool<IsObjectSpawnComponent>();
            _isLampSpawnComponentPool = _world.GetPool<IsLampSpawnComponent>();
            _poolService = Service<IPoolService>.Get();
            _patternService = Service<IPatternService>.Get();
        }

        public void Run(IEcsSystems systems)
        {
            ref TreadmillComponent treadmillComponent =
                ref _treadmillComponentPool.Get(_treadmillFilter.GetRawEntities()[0]);

            ref TransformComponent treadmillTransformComponent =
                ref _transformComponentPool.Get(_treadmillFilter.GetRawEntities()[0]);

            if (treadmillTransformComponent.Value is null) return;

            if (treadmillTransformComponent.Value.childCount < treadmillComponent.StartPlatformCount)
            {
                SpawnPatternStaticData pattern = _patternService.GetRandomPattern();
                GameObjectsTypeId id = (GameObjectsTypeId)Enum.Parse(typeof(GameObjectsTypeId), pattern.TunnelPrefabName);
                GameObject platformFromPool = _poolService.Get(id);

                _platformFilter = _world.Filter<IsPlatformComponent>().Exc<IsMoveComponent>().End();
                _lastPlatformFilter = _world.Filter<IsPlatformComponent>().Inc<IsLastPlatformComponent>().End();

                foreach (int platformEntity in _platformFilter)
                {
                    ref TransformComponent transformComponent = ref _transformComponentPool.Get(platformEntity);

                    platformFromPool.transform.position =
                        new Vector3(treadmillComponent.SpawnPlatformPoint.x,
                            treadmillComponent.SpawnPlatformPoint.y,
                            treadmillComponent.SpawnPlatformPoint.z + treadmillComponent.PlatformLength);
                    transformComponent.Value = platformFromPool.transform;
                    platformFromPool.transform.parent = treadmillTransformComponent.Value;
                    _isMoveComponentPool.Add(platformEntity);
                    _isLastPlatformComponentPool.Add(platformEntity);
                    _isObjectSpawnComponentPool.Add(platformEntity);
                    
                    ref IsObjectSpawnComponent isObjectSpawnComponent = ref _isObjectSpawnComponentPool.Get(platformEntity);
                    isObjectSpawnComponent.Pattern = pattern;
                    
                    if(!_isLampSpawnComponentPool.Has(platformEntity))
                        _isLampSpawnComponentPool.Add(platformEntity);
                    _isLastPlatformComponentPool.Del(_lastPlatformFilter.GetRawEntities()[0]);

                    if (platformFromPool.TryGetComponent(out PlatformView platformView))
                        treadmillComponent.Platforms.Enqueue(platformView);
                }
            }
        }
    }
}