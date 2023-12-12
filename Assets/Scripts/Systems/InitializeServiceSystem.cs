using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;


namespace  HalfDiggers.Runner
{
    public sealed class InitializeServiceSystem : IEcsInitSystem
    {
        private readonly IPoolService _poolService;
        private readonly IPatternService _patternService;

        public InitializeServiceSystem(IPoolService poolService, IPatternService patternService)
        {
            _poolService = poolService;
            _patternService = patternService;
        }

        public void Init(IEcsSystems systems)
        {
            Service<ITimeService>.Set(new UnityTimeService());
            Service<GameObjectAssetLoader>.Set(new GameObjectAssetLoader());
            Service<ScriptableObjectAssetLoader>.Set(new ScriptableObjectAssetLoader());

            if (Application.isEditor)
                Service<IInputService>.Set(new KeyboardInputService());
            else
                Service<IInputService>.Set(new SwipeService());
            
            Service<IPoolService>.Set(_poolService);
            Service<IPatternService>.Set(_patternService);
        }
    }
    
}
