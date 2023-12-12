using UnityEngine;



namespace HalfDiggers.Runner
{
    public sealed class TransformView : BaseView
    {
        [SerializeField] private Transform _transform;

        public Transform Transform => _transform;
    }
}