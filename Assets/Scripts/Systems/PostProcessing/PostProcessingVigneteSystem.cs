using Leopotam.EcsLite;
using UnityEngine;


namespace HalfDiggers.Runner
{
    public class PostProcessingVigneteSystem:IEcsInitSystem,IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<PostProcessingComponent> _postProcessingObjectPool;
  
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PostProcessingComponent>().End();
            _postProcessingObjectPool = _world.GetPool<PostProcessingComponent>();
        }

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var postprocessing = ref _postProcessingObjectPool.Get(entity);
                if(postprocessing.VignetteValue==null) return;
                float time = Time.deltaTime*postprocessing.FadeSpeed;
                var delta=  Mathf.SmoothStep(postprocessing.VignetteValue.intensity.value, postprocessing.IntensivityValue, time);

                postprocessing.VignetteValue.intensity.value = delta;
                
            }
        }
    }
}