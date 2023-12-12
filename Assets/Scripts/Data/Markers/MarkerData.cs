using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HalfDiggers.Runner
{
    [CreateAssetMenu(menuName = "Marker/MarkerData", fileName = "New MarkerData", order = 51)]
    public class MarkerData : ScriptableObject
    {
        public GameObjectsTypeId GameObjectsTypeId;
        public AssetReferenceGameObject MakerGameObject;
    }
}