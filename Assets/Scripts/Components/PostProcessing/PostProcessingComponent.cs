using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace HalfDiggers.Runner
{
    public struct PostProcessingComponent
    {
        public Volume VolumeValue;
        public Vignette VignetteValue;
        public Color ColorValue;
        public float InitialSmoothnessValue;
        public float InitialIntensivityValue;
        public float TwoLifeIntensivityValue; 
        public float OneLifeIntensivityValue;
        public float IntensivityValue;
        public float FadeSpeed;
        public bool IsRoundedValue;
        
    }
}
