using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{
    public class MenuSystems
    {
        public MenuSystems(EcsSystems systems)
        {
            systems
                .Add(new PauseMenuLoadSystem())
                .Add(new PauseMenuInitSystem())
                .Add(new PauseMenuBuildSystem());
            
            systems
                .Add(new PauseMenuHandleLoadSystem())
                .Add(new PauseMenuHandInitSystem())
                .Add(new PauseMenuCallBackSystem())
                .Add(new PauseMenuHandSystem())
                .Add(new PauseMenuHandBuildSystem());

            systems
                .Add(new DeathMenuLoadSystem())
                .Add(new DeathMenuInitSystem())
                .Add(new DeathMenuSystem())
                .Add(new DeathMenuCallBackSystem())
                .Add(new DeathMenuBuildSystem());
            
            systems
                .Add(new SettingMenuLoadSystem())
                .Add(new SettingMenuInitSystem())
                .Add(new SettingMenuSystem())
                .Add(new SettingMenuCallBackSystem())
                .Add(new SettingSliderVolumeSystem())
                .Add(new SettingMenuBuildSystem());
        }
    }
}