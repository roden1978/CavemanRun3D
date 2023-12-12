using UnityEngine;
using UnityEngine.AddressableAssets;



namespace HalfDiggers.Runner
{
    [CreateAssetMenu(fileName = nameof(ExampleData),
        menuName = EditorMenuConstants.CREATE_TRANSPORT_SHIP_MENU_NAME + nameof(ExampleData))]
    public class ExampleData : ScriptableObject
    {
        [Header("Prefabs:")]
        public AssetReferenceGameObject TransportShipPrefab;

       [Header("Other:")]
        public TestEnumType testEnumType;
        public ExampleCharacteristics exampleCharacteristic;
        public ExampleMovingData exampleMovingData;
    }
}
