using StaticData;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public class PickableObjectMarker : MonoBehaviour
    {
        [field: SerializeField]
        public PickableObjectStaticData PickableObjectStaticData { get; private set; }
    }
}