using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;


namespace HalfDiggers.Runner
{
    public class PauseMenuHandBuildSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<CloseBtnCommand> _closMenuPool;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<CloseBtnCommand>().Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _closMenuPool = world.GetPool<CloseBtnCommand>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var prefabComponent = ref _prefabPool.Get(entity);
                var gameObject = Object.Instantiate(prefabComponent.Value);
                var canvas = GameObject.FindObjectOfType<Canvas>();
                gameObject.transform.parent = canvas.transform;
                gameObject.GetComponent<RectTransform>().anchorMin = Vector2.one;
                gameObject.GetComponent<RectTransform>().anchorMax = Vector2.one;
                gameObject.GetComponent<RectTransform>().pivot = Vector2.one;
                gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                ref var menu = ref _closMenuPool.Get(entity);
                menu.MenuValue = gameObject.GetComponent<TransformView>().gameObject;
                _prefabPool.Del(entity);
            }
        }
    }
}