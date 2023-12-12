using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using StaticData;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class InstantiatePickableObjectsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private IPoolService _poolService;
        private IPatternService _patternService;
        private EcsFilter _platformFilter;
        private EcsPool<IsPlatformComponent> _isPlatformComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<IsObjectSpawnComponent> _isObjectSpawnComponentPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _platformFilter = world.Filter<IsPlatformComponent>()
                .Inc<IsLastPlatformComponent>()
                .Inc<IsObjectSpawnComponent>()
                .End();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _isObjectSpawnComponentPool = world.GetPool<IsObjectSpawnComponent>();
            _isPlatformComponentPool = world.GetPool<IsPlatformComponent>();
            _poolService = Service<IPoolService>.Get();
            _patternService = Service<IPatternService>.Get();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _platformFilter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                ref IsPlatformComponent isPlatformComponent = ref _isPlatformComponentPool.Get(entity);
                ref IsObjectSpawnComponent isObjectSpawnComponent = ref _isObjectSpawnComponentPool.Get(entity);

                foreach (PickableObjectSpawnerData objectSpawner in isObjectSpawnComponent.Pattern.ObjectSpawners)
                {
                    GameObject spawnerGo = _poolService.Get(GameObjectsTypeId.ObjectSpawner);
                    spawnerGo.gameObject.transform.parent = transformComponent.Value;
                    spawnerGo.transform.localPosition = objectSpawner.LocalPosition;
                    spawnerGo.transform.rotation = objectSpawner.Rotation;

                    PickableObjectSpawner spawner = spawnerGo.GetComponent<PickableObjectSpawner>();
                    spawner.Construct(objectSpawner.Id, objectSpawner.GameObjectsTypeId, _poolService,
                        transformComponent.Value, objectSpawner.LocalPosition, objectSpawner.Rotation);

                    GameObject pickableObject = spawner.Spawn();

                    if (pickableObject is not null)
                        isPlatformComponent.PickableObjects.Add(pickableObject);

                    _poolService.Return(spawnerGo);
                }

                _isObjectSpawnComponentPool.Del(entity);
            }
        }
    }
}