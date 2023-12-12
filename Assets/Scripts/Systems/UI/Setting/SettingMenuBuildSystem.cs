﻿using Leopotam.EcsLite;
using UnityEngine;


namespace HalfDiggers.Runner
{
    public class SettingMenuBuildSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<IsSettingMenu> _isMenuPool;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<IsSettingMenu>().Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _isMenuPool = world.GetPool<IsSettingMenu>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var prefabComponent = ref _prefabPool.Get(entity);
                var gameObject = Object.Instantiate(prefabComponent.Value);
                var canvas = GameObject.FindObjectOfType<Canvas>();
                gameObject.transform.parent = canvas.transform;
                ref var menu = ref _isMenuPool.Get(entity);
                gameObject.GetComponent<RectTransform>().anchoredPosition=Vector2.left;
                menu.MenuValue = gameObject.GetComponent<TransformView>().gameObject;
                gameObject.transform.localPosition = Vector3.zero;
              menu.MenuValue.SetActive(false);
               _prefabPool.Del(entity);
            }
        }
    }
}