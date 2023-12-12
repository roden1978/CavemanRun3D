using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    public class PlayerSystems
    {
        public PlayerSystems(EcsSystems systems)
        {
            systems
                .Add(new PlayerSystem())
                .Add(new PlayerInitSystem())
                .Add(new PlayerBuildSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerMoveSystem())
                .Add(new PlayerJumpSystem())
                .Add(new PlayerBounceSystem());
        }
    }
}