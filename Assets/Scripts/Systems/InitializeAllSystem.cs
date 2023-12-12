using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    internal class InitializeAllSystem
    {
        public InitializeAllSystem(EcsSystems systems, IPoolService poolService, IPatternService patternService)
        {
            systems
                .Add(new InitializeServiceSystem(poolService, patternService))
                .Add(new LoadPrefabSystem())
                .Add(new LoadDataByNameSystem());
            new CommonSystems(systems);
            systems.Add(new LoadTreadmillDataSystem())
                .Add(new TreadmillInitSystem())
                .Add(new TreadmillBuildSystem());
            new PlayerSystems(systems);
            new CameraSystems(systems);
            new MenuSystems(systems);
            //new CoinSystems(systems);
            new PostProcessingSystems(systems);
            systems.Add(new OnGroundSystem());
            //Use Gravity 
            systems.Add(new GravitySystem());
            systems.Add(new MovePlatformSystem())
                .Add(new CheckPositionPlatformSystem())
                .Add(new RespawnPlatformSystem())
                .Add(new InstantiateLampSystem())
                .Add(new InstantiatePickableObjectsSystem())
                .Add(new AccelerationPlatformSystem());
            new SoundSystems(systems);
            systems.Add(new DataManagerSystem())
                .Add(new DEBUG_TestDataManagerSystems());
            new DeathSystems(systems);
            //Trigger
            systems.Add(new TriggerSystem());

        }
    }
}