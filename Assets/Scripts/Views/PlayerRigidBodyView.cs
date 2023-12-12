using UnityEngine;

namespace HalfDiggers.Runner
{
    public sealed class PlayerRigidBodyView: BaseView
    {
        [SerializeField] private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;
    }
}