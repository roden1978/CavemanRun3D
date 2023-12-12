using UnityEngine;
using UnityEngine.AddressableAssets;


namespace HalfDiggers.Runner
{
    [CreateAssetMenu(fileName = nameof(PostProcessingData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(PostProcessingData))]
    public class PostProcessingData : ScriptableObject
    {
        [Header("Volume")] 
        public AssetReferenceGameObject Volume;
        [Header("Vignette")] 
        public Color VignetteColor=Color.black;
        public float InitialSmoothnessValue=1f;
        public float InitialIntensivity=0.0f;
        public float TwoLifeIntensivity=0.2f;
        public float OneLifeIntensivity=0.5f;
        public float FadeSpeed = 10;
        public bool IsRounded = true;
    }
}