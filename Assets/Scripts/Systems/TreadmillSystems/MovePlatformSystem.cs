using Leopotam.EcsLite;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class MovePlatformSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _platformFilter;
        private EcsFilter _treadmillFilter;
        private EcsPool<TreadmillComponent> _treadmillComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsFilter _lastPlatformFilter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _platformFilter = _world.Filter<IsPlatformComponent>()
                                    .Inc<TransformComponent>()
                                    .Inc<IsMoveComponent>()
                                    .End();
            _treadmillFilter = _world.Filter<IsTreadmillComponent>().Inc<TreadmillComponent>().End();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _treadmillComponentPool = _world.GetPool<TreadmillComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            ref TreadmillComponent treadmillComponent =
                ref _treadmillComponentPool.Get(_treadmillFilter.GetRawEntities()[0]);

            foreach (int platformEntity in _platformFilter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(platformEntity);

                if (transformComponent.Value.gameObject.TryGetComponent(out PlatformView platform))
                {
                    Vector3 position = transformComponent.Value.position;
                    position -= new Vector3(0, 0, treadmillComponent.Speed * Time.deltaTime);
                    transformComponent.Value.localPosition = position;
                    platform.Position = position;
                }
            }

            _lastPlatformFilter = _world.Filter<IsPlatformComponent>().Inc<IsLastPlatformComponent>().End();

            ref TransformComponent lastTransform =
                ref _transformComponentPool.Get(_lastPlatformFilter.GetRawEntities()[0]);

            if (lastTransform.Value is null) return;

            treadmillComponent.SpawnPlatformPoint = lastTransform.Value.position;
        }
    }
}