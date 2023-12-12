using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    public sealed class CoinSystems
    {
        public CoinSystems(EcsSystems systems)
        {
            systems
                .Add(new CoinSystem())
                .Add(new CoinInitSystem())
                .Add(new CoinBuildSystem());
        }

    }
}