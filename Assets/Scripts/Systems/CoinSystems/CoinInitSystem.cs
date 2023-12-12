using HalfDiggers.Runner.Coin;
using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    public class CoinInitSystem: IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<CoinStartPositionComponent> _coinStartPositionComponentPool;
        
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsCoinComponent>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _coinStartPositionComponentPool = _world.GetPool<CoinStartPositionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is CoinLoadData dataInit)
                {
                    ref var loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.Coin;
                    
                    ref var coinStartPositionComponent = ref _coinStartPositionComponentPool.Add(entity);
                    coinStartPositionComponent.Value = dataInit.StartPosition;
                }
                _scriptableObjectPool.Del(entity);
            }
        }
    }
}