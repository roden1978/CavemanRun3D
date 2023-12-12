using UnityEngine;
using UnityEngine.Rendering;


namespace HalfDiggers.Runner
{
    public sealed class PostProcessingView : BaseView
    {
        [SerializeField] private Volume _volume;

        public Volume Volume => _volume;
    }
}