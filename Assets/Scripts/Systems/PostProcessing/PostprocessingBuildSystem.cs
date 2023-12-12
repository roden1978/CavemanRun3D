using Leopotam.EcsLite;
using UnityEngine;


namespace HalfDiggers.Runner
{
    public class PostprocessingBuildSystem: IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<PostProcessingComponent> _postProcessingPool;
     
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<PostProcessingComponent>().Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _postProcessingPool = world.GetPool<PostProcessingComponent>();
        }

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var prefabComponent = ref _prefabPool.Get(entity);
                var gameObject = Object.Instantiate(prefabComponent.Value);
                gameObject.transform.position = Vector3.zero;
                ref var postProcessing = ref _postProcessingPool.Get(entity);
                
                postProcessing.VolumeValue = gameObject.GetComponent<PostProcessingView>().Volume;
                postProcessing.VolumeValue.profile.TryGet(out postProcessing.VignetteValue);
                
               _prefabPool.Del(entity);
            }
        }
    }
}