using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class OnGroundSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const int GroundLayerMask = 1 << 6;
        
        private EcsFilter _filter;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<IsOnGroundComponent> _isOnGroundComponentPool;
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private Collider[] _colliders = new Collider[5];

        private readonly RaycastHit[] _results;
        
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<GravityComponent>().Inc<TransformComponent>().End();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _isOnGroundComponentPool = world.GetPool<IsOnGroundComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                if (_skinnedMeshRenderer is null)
                {
                    ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                    
                    if (transformComponent.Value is null) return;
                    
                    _skinnedMeshRenderer =
                        transformComponent.Value.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
                    return;
                }
                ref TransformComponent trComponent  = ref _transformComponentPool.Get(entity);
                
                if (Contact(ref trComponent) && !_isOnGroundComponentPool.Has(entity))
                    _isOnGroundComponentPool.Add(entity);
                
                if (!Contact(ref trComponent) && _isOnGroundComponentPool.Has(entity))
                    _isOnGroundComponentPool.Del(entity);
            }
        }
        
        private bool Contact(ref TransformComponent transformComponent)
        {
            int count = Physics.OverlapBoxNonAlloc(transformComponent.Value.position, new Vector3(.5f, .5f, .5f), _colliders, Quaternion.identity, GroundLayerMask);
            return count > 0;
            //return Physics.CheckBox(transformComponent.Value.position, , GroundLayerMask);
        }
        
    }
}