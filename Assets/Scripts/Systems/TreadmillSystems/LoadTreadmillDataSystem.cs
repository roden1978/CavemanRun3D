using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    public class LoadTreadmillDataSystem : IEcsInitSystem
    {
        private EcsPool<IsTreadmillComponent> _isTreadmillComponentPool;
        private EcsPool<TreadmillComponent> _treadmillComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            int entity = world.NewEntity();
            
            _isTreadmillComponentPool = world.GetPool<IsTreadmillComponent>();
            _isTreadmillComponentPool.Add(entity);
            
            _treadmillComponentPool = world.GetPool<TreadmillComponent>();
            _treadmillComponentPool.Add(entity);
            
            _transformComponentPool = world.GetPool<TransformComponent>();
            _transformComponentPool.Add(entity);

            EcsPool<LoadDataByNameComponent> loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref LoadDataByNameComponent loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.STATIC_TREADMILL_DATA;
        }
    }
}