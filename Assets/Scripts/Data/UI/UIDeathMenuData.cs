using UnityEngine;
using UnityEngine.AddressableAssets;


namespace HalfDiggers.Runner
{
    [CreateAssetMenu(fileName = nameof(UIDeathMenuData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(UIDeathMenuData))]
    public class UIDeathMenuData : ScriptableObject
    {
        [Header("Menu")] 
        public AssetReferenceGameObject Menu; 

    }
}