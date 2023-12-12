using Leopotam.EcsLite;



namespace HalfDiggers.Runner
{
    public sealed class TestSmoothMovingSystems
    {
        public TestSmoothMovingSystems(EcsSystems systems)
        {
            systems
                .Add(new DEBUG_TestSmoothMovingSystems())
                .Add(new TestSmoothMovingInitSystem())
                .Add(new TestSmoothMovingSystem())
                .Add(new TestSmoothMovingCheckArrivalSystem())
                .Add(new TestSmoothMovingTimerParkingSystem())
                .Add(new TestSmoothMovingBuildGameObjectSystem());
        }
    }
}