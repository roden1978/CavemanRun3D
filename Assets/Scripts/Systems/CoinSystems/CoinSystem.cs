using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    public class CoinSystem: IEcsInitSystem
    {
        private EcsPool<IsCoinComponent> _isCoinPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isCoinPool = world.GetPool<IsCoinComponent>();
            _isCoinPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.COIN_SHARED_DATA;
        }
    }
}