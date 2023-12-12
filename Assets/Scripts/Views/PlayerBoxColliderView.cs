using UnityEngine;

namespace HalfDiggers.Runner
{
    public sealed class PlayerBoxColliderView : BaseView

    {
        [SerializeField] private BoxCollider _boxCollider;

        public BoxCollider BoxCollider => _boxCollider;
    }
}