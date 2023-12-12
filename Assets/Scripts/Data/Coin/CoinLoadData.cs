using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HalfDiggers.Runner.Coin
{
    [CreateAssetMenu(fileName = nameof(CoinLoadData),
        menuName = EditorMenuConstants.CREATE_COIN + nameof(CoinLoadData), order = 3)]
    
    public class CoinLoadData:ScriptableObject
    {
        [Header("Prefabs:")]
        public AssetReferenceGameObject Coin;
        
        [Header("Positions:")]
        public Vector3 StartPosition;
    }
}