using Leopotam.EcsLite;
using UnityEngine;


namespace HalfDiggers.Runner
{
    
    public class CollisionCheckerView : MonoBehaviour, IHaveActor
    {
        [SerializeField] private Transform _collisionPoint;
        public IActor Actor { get; set; }
        public EcsWorld EcsWorld { get; set; }
        private const int LAYER_MASK = 1 << 7;
        private readonly Collider[] _colliders = new Collider[1];


        private void Update()
        {
            CollectObjectsCollisionOverlap();
        }

        private void CollectObjectsCollisionOverlap()
        {
            int count = Physics.OverlapSphereNonAlloc(_collisionPoint.position, 1f, _colliders, LAYER_MASK);
            if (count > 0)
            {
                if (_colliders[0].TryGetComponent(out IActor otherActor))
                {
                    CollisionHandle(otherActor);
                }
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IActor otherActor))
            {
                CollisionHandle(otherActor);
            }
        }

        
        private void OnTriggerEnter(Collider otherCollider)
        {
            if (otherCollider.gameObject.TryGetComponent(out IActor otherActor))
            {
                CollisionHandle(otherActor);
            }
        }
        

        private void CollisionHandle(IActor otherActor)
        {
            int hit = EcsWorld.NewEntity();
            EcsPool<HitComponent> hitPool = EcsWorld.GetPool<HitComponent>();
            hitPool.Add(hit);
            ref HitComponent hitComponent = ref hitPool.Get(hit);

            hitComponent.FirstEntity = Actor.Entity;
            hitComponent.OtherEntity = otherActor.Entity;

            switch (otherActor)
            {
                case CoinActor:
                    Debug.Log("Is Coin Actor");
                    EcsPool<IsHitCoinsComponent> hitCoinsPool = EcsWorld.GetPool<IsHitCoinsComponent>();
                    hitCoinsPool.Add(hit);

                    break;
                case PillarLampActor:
                    Debug.Log("Is Pillar Actor");
                    AddHitPillarComponent(hit);
                    AddBounceComponent();
                    break;

                case WrenchActor:
                    Debug.Log("Is Wrench Actor");
                    EcsPool<IsHitWrenchComponent> hitWrenchPool = EcsWorld.GetPool<IsHitWrenchComponent>();
                    hitWrenchPool.Add(hit);
                    break;

                default: break;
            }
            
            otherActor.Handle();
        }

        private void AddBounceComponent()
        {
            EcsFilter filter = EcsWorld.Filter<IsPlayerComponent>().Inc<IsPlayerMoveComponent>().End();
            EcsPool<IsPlayerMoveComponent> isPlayerMoveComponentPool =
                EcsWorld.GetPool<IsPlayerMoveComponent>();
            EcsPool<IsPlayerBounceComponent> isPlayerBounceComponentPool =
                EcsWorld.GetPool<IsPlayerBounceComponent>();

            int entity = filter.GetRawEntities()[0];
            if (isPlayerMoveComponentPool.Has(entity))
            {
                isPlayerBounceComponentPool.Add(entity);

                ref IsPlayerBounceComponent isPlayerBounceComponent =
                    ref isPlayerBounceComponentPool.Get(entity);
                ref IsPlayerMoveComponent isPlayerMoveComponent =
                    ref isPlayerMoveComponentPool.Get(entity);
                isPlayerBounceComponent.ReturnPosition = isPlayerMoveComponent.StartMovePosition;

                isPlayerMoveComponentPool.Del(entity);
            }
        }

        private void AddHitPillarComponent(int hit)
        {
            EcsPool<IsHitPillarComponent> hitPillarsPool = EcsWorld.GetPool<IsHitPillarComponent>();
            hitPillarsPool.Add(hit);
        }
    }
}