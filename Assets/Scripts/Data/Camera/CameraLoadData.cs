using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HalfDiggers.Runner
{


    [CreateAssetMenu(fileName = nameof(CameraLoadData),
        menuName = EditorMenuConstants.CREATE_TRANSPORT_SHIP_MENU_NAME + nameof(CameraLoadData))]
    public class CameraLoadData : ScriptableObject
    {
        [Header("Prefabs:")]
        public AssetReferenceGameObject Camera;
        [Header("Positions:")]
        public Vector3 StartPosition;
        [Header("Rotations:")]
        public Vector3 StartRotation;
        [Range(0f, 1f)]
        public float CameraSmoothness;
    }
}

