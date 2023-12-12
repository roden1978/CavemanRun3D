using Leopotam.EcsLite;


namespace HalfDiggers.Runner
{
    public class PostProcessingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<PostProcessingComponent> _postProcessingObjectPool;
        
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PostProcessingComponent>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is PostProcessingData dataInit)
                {
                    ref var loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.Volume;

                    var postprocessingPool = _world.GetPool<PostProcessingComponent>();
                    ref var postprocessing = ref postprocessingPool.Get(entity);
                    postprocessing.InitialSmoothnessValue = dataInit.InitialSmoothnessValue;
                    postprocessing.InitialIntensivityValue = dataInit.InitialIntensivity;
                    postprocessing.OneLifeIntensivityValue = dataInit.OneLifeIntensivity;
                    postprocessing.TwoLifeIntensivityValue = dataInit.TwoLifeIntensivity;
                    postprocessing.ColorValue = dataInit.VignetteColor;
                    postprocessing.IsRoundedValue = dataInit.IsRounded;
                    postprocessing.FadeSpeed = dataInit.FadeSpeed;

                    postprocessing.IntensivityValue = postprocessing.InitialIntensivityValue;
                }

                _scriptableObjectPool.Del(entity);
            }
        }
    }
}