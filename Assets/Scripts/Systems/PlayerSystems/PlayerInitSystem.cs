﻿using Leopotam.EcsLite;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class PlayerInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<PlayerStartPositionComponent> _playerStartPositionComponentPool;
        private EcsPool<SpeedVectorComponent> _speedVectorComponentPool;
        private EcsPool<DestinationComponent> _destinationComponentPool;
        private EcsPool<PlatformSideComponent> _platformSideComponentPool;
        private EcsPool<GravityComponent> _gravityComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<PlayerInputComponent> _playerInputComponentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsPlayerComponent>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _playerStartPositionComponentPool = _world.GetPool<PlayerStartPositionComponent>();
            _platformSideComponentPool = _world.GetPool<PlatformSideComponent>();
            _destinationComponentPool = _world.GetPool<DestinationComponent>();
            _speedVectorComponentPool = _world.GetPool<SpeedVectorComponent>();
            _gravityComponentPool = _world.GetPool<GravityComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _playerInputComponentPool = _world.GetPool<PlayerInputComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is StaticPlayerData dataInit)
                {
                    ref LoadPrefabComponent loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.Player;

                    ref PlayerStartPositionComponent playerStartPositionComponent =
                        ref _playerStartPositionComponentPool.Add(entity);
                    playerStartPositionComponent.Value = dataInit.StartPosition;

                    ref SpeedVectorComponent speedVectorComponent = ref _speedVectorComponentPool.Add(entity);
                    speedVectorComponent.Value = new Vector2(dataInit.MoveSpeed,dataInit.JumpSpeed);

                    ref DestinationComponent destinationComponent = ref _destinationComponentPool.Add(entity);
                    destinationComponent.Value = new Vector2(dataInit.MoveDistance, dataInit.JumpHeigth);

                    ref PlatformSideComponent platformSideComponent = ref _platformSideComponentPool.Add(entity);
                    platformSideComponent.PlatformSide = PlatformSide.Center;

                    _gravityComponentPool.Add(entity);
                    _transformComponentPool.Add(entity);
                    _playerInputComponentPool.Add(entity);
                }

                _scriptableObjectPool.Del(entity);
            }
        }
    }
}