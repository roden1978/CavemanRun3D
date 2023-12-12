using Leopotam.EcsLite;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class CoinBuildSystem: IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<CoinStartPositionComponent> _coinStartPositionComponentPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<IsCoinComponent>().Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _coinStartPositionComponentPool = world.GetPool<CoinStartPositionComponent>();
           
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var prefabComponent = ref _prefabPool.Get(entity);
                ref var transformComponent = ref _transformComponentPool.Add(entity);
                ref var coinPosition = ref _coinStartPositionComponentPool.Get(entity);
                

                var gameObject = Object.Instantiate(prefabComponent.Value);
                transformComponent.Value =  gameObject.GetComponent<CoinView>().Transform;
                gameObject.transform.position = coinPosition.Value;
                var actor = gameObject.GetComponent<Actor>();
                actor.AddEntity(entity);
                _prefabPool.Del(entity);
            }
        }
    }
}