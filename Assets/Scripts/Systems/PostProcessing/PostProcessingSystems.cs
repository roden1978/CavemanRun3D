using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    public class PostProcessingSystems
    {
        
        public PostProcessingSystems(EcsSystems systems)
        {
            systems
                .Add(new DEBUG_TestPostProcessingSystems())
                .Add(new PostProcessingSystem())
                .Add(new PostProcessingVigneteSystem())
                .Add(new PostProcessingInitSystem())
                .Add(new PostprocessingBuildSystem());
        }
    }
}