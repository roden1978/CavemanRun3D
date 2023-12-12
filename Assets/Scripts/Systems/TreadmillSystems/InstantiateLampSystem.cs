using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using StaticData;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class InstantiateLampSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _platformFilter;
        private IPoolService _poolService;
        private IPatternService _patternService;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<IsPlatformComponent> _isPlatformComponentPool;
        private EcsPool<IsLampSpawnComponent> _isLampSpawnComponentPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _platformFilter = world.Filter<IsPlatformComponent>()
                .Inc<IsLampSpawnComponent>()
                .End();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _isPlatformComponentPool = world.GetPool<IsPlatformComponent>();
            _isLampSpawnComponentPool = world.GetPool<IsLampSpawnComponent>();
            _poolService = Service<IPoolService>.Get();
            _patternService = Service<IPatternService>.Get();
        }
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _platformFilter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                ref IsPlatformComponent isPlatformComponent = ref _isPlatformComponentPool.Get(entity);
                
                IEnumerable<SpawnPatternStaticData> patterns = _patternService.GetPatternsGroup(PatternGroups.Lamp);

                foreach (SpawnPatternStaticData pattern in patterns)
                {
                    foreach (PickableObjectSpawnerData objectSpawner in pattern.ObjectSpawners)
                    {
                        GameObject spawnerGo = _poolService.Get(GameObjectsTypeId.ObjectSpawner);
                        spawnerGo.gameObject.transform.parent = transformComponent.Value;
                        spawnerGo.transform.localPosition = objectSpawner.LocalPosition;
                        spawnerGo.transform.rotation = objectSpawner.Rotation;

                        PickableObjectSpawner spawner = spawnerGo.GetComponent<PickableObjectSpawner>();
                        spawner.Construct(objectSpawner.Id, objectSpawner.GameObjectsTypeId, _poolService,
                            transformComponent.Value, objectSpawner.LocalPosition, objectSpawner.Rotation);
                        
                        GameObject lamp = spawner.Spawn();

                        if (lamp is not null)
                        {
                            switch (objectSpawner.GameObjectsTypeId)
                            {
                                case GameObjectsTypeId.PillarLamp:
                                    isPlatformComponent.PillarLamps.Add(lamp);
                                    break;
                                case GameObjectsTypeId.WallLamp:
                                    isPlatformComponent.WallLamps.Add(lamp);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        _poolService.Return(spawnerGo);
                    }
                }
                _isLampSpawnComponentPool.Del(entity);
            }
        }
    }
}