using HalfDiggers.Runner;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "New PickableObjectData", menuName = "StaticData/PickableObjectData")]
    public class PickableObjectStaticData : ScriptableObject
    {
        public GameObjectsTypeId GameObjectsTypeId;
    }
}