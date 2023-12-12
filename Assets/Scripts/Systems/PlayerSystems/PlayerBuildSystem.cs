﻿using Leopotam.EcsLite;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class PlayerBuildSystem: IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<PlayerStartPositionComponent> _playerStartPositionComponentPool;
        private EcsPool<PlayerBoxColliderComponent> _playerBoxColliderComponentPool;
        private EcsPool<PlayerRigidBodyComponent> _playerRigidBodyComponentPool;


        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<IsPlayerComponent>().Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _playerStartPositionComponentPool = world.GetPool<PlayerStartPositionComponent>();
            _playerBoxColliderComponentPool = world.GetPool<PlayerBoxColliderComponent>();
            _playerRigidBodyComponentPool = world.GetPool<PlayerRigidBodyComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();
            foreach (int entity in _filter)
            {
                ref PrefabComponent prefabComponent = ref _prefabPool.Get(entity);
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                ref PlayerStartPositionComponent playerPosition = ref _playerStartPositionComponentPool.Get(entity);
                ref PlayerRigidBodyComponent playerRigidBodyComponent = ref _playerRigidBodyComponentPool.Add(entity);
                ref PlayerBoxColliderComponent playerBoxColliderComponent = ref _playerBoxColliderComponentPool.Add(entity);

                GameObject gameObject = Object.Instantiate(prefabComponent.Value);
                transformComponent.Value = gameObject.GetComponent<TransformView>().Transform;
                gameObject.transform.position = playerPosition.Value;
                gameObject.GetComponentInChildren<CollisionCheckerView>().EcsWorld = ecsWorld;
                gameObject.GetComponent<IActor>().AddEntity(entity);
                playerBoxColliderComponent.PlayerCollider = gameObject.GetComponent<BoxCollider>();
                playerRigidBodyComponent.PlayerRigidbody = gameObject.GetComponent<Rigidbody>();
               _prefabPool.Del(entity);
            }
        }
    }
}