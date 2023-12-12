using Leopotam.EcsLite;


namespace HalfDiggers.Runner
{
    public class PostProcessingInitSystem : IEcsInitSystem
    {
        private EcsPool<PostProcessingComponent> _isPostProcessing;

        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isPostProcessing = world.GetPool<PostProcessingComponent>();
            _isPostProcessing.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.POST_PROCESSING_VOLUME_PREFAB_NAME;
        }
    }
}