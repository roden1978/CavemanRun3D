using HalfDiggers.Runner;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class WrenchView : BaseView
    {
        [SerializeField] private Transform _transform;

        public Transform Transform => _transform;
    }
    
}