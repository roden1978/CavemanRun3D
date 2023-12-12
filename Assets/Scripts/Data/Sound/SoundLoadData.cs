using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HalfDiggers.Runner
{


    [CreateAssetMenu(fileName = nameof(SoundLoadData),
        menuName = EditorMenuConstants.CREATE_TRANSPORT_SHIP_MENU_NAME + nameof(SoundLoadData))]
    public class SoundLoadData : ScriptableObject
    {
        [Header("Prefabs:")]
        public AssetReferenceGameObject Source;
        [Header("Positions:")]
        public Vector3 StartPosition;
        [Header("Tack List:")]
        public AudioClip[] Tracks;
        [Header("First track number")]
        public int FirstTrackNumber;
    }
}

