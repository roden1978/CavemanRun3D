using System.Collections.Generic;
using GameObjectsScripts;
using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    public class TreadmillInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<TreadmillComponent> _treadmillComponentPool;
        private EcsPool<CreateGameObjectComponent> _createGameObjectComponentPool;
        
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<IsTreadmillComponent>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = world.GetPool<ScriptableObjectComponent>();
            _treadmillComponentPool = world.GetPool<TreadmillComponent>();
            _createGameObjectComponentPool = world.GetPool<CreateGameObjectComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is TreadmillData dataInit)
                {
                    ref TreadmillComponent treadmillComponent = ref _treadmillComponentPool.Get(entity);
                    treadmillComponent.Speed = dataInit.Speed;
                    treadmillComponent.AccelerationInterval = dataInit.AccelerationInterval;
                    treadmillComponent.AccelerationValue = dataInit.AccelerationValue;
                    treadmillComponent.PlatformsBeforePlayer = dataInit.PlatformsBeforePlayer;
                    treadmillComponent.StartPlatformCount = dataInit.StartPlatformCount;
                    treadmillComponent.Platforms = new Queue<PlatformView>(dataInit.StartPlatformCount);
                    treadmillComponent.UsingPlatform = dataInit.UsingPlatform;
                    _createGameObjectComponentPool.Add(entity);
                }

                _scriptableObjectPool.Del(entity);
            }
        }
    }
}