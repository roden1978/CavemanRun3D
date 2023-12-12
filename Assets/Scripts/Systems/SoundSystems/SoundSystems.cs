﻿using Leopotam.EcsLite;

namespace HalfDiggers.Runner
{


    public sealed class SoundSystems
    {
        public SoundSystems(EcsSystems systems)
        {
            systems
                .Add(new SoundSystem())
                .Add(new SoundInitSystem())
                .Add(new SoundBuildSystem())
                //.Add(new DEBUG_TestSoundSystems())
                .Add(new SoundMusicSwitchSystem())
                .Add(new SoundCatchSystem());
        }
    }
}