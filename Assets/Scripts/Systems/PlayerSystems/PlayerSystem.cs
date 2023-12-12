using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    public class PlayerSystem: IEcsInitSystem
    {
        private EcsPool<IsPlayerComponent> _isPlayerPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isPlayerPool = world.GetPool<IsPlayerComponent>();
            _isPlayerPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.STATIC_PLAYER_DATA;
        }
    }
}