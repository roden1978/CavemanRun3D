using UnityEngine;
using UnityEngine.AddressableAssets;


namespace HalfDiggers.Runner
{
    [CreateAssetMenu(fileName = nameof(UISettingMenuData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(UISettingMenuData))]
    public class UISettingMenuData : ScriptableObject
    {
        [Header("Menu")] 
        public AssetReferenceGameObject Menu; 

    }
}