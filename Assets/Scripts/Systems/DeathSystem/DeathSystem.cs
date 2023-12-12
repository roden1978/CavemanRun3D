using Leopotam.EcsLite;
using LeopotamGroup.Globals;


namespace HalfDiggers.Runner
{
    public class DeathSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<IsPlayerDeathComponent> _isPlayerDeathPool;
        private EcsPool<ShowDeathMenu> _isBtnShowMenu;
        private PlayerSharedData _sharedData;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;

            _filter = world.Filter<IsPlayerComponent>().Exc<IsPlayerDeathComponent>().End();
            _isPlayerDeathPool = world.GetPool<IsPlayerDeathComponent>();
            _isBtnShowMenu = world.GetPool<ShowDeathMenu>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
               
                if (_sharedData.GetPlayerCharacteristic.GetLives.GetCurrrentLives <= 0)
                {
                    Extensions.AddPool<IsPlayerDeathComponent>(systems, entity);
                    Extensions.AddPool<ShowDeathMenu>(systems, entity);
                    var timeServise = Service<ITimeService>.Get();
                    timeServise.Pause();

                    //Save PlayerStats
                    systems.GetWorld().GetPool<IsManagePlayerStatsComponent>().Add(entity)
                        .dataAction = DataManageEnumType.Save;
                    //Add Sound
                    ref var isHitSoundComponent = ref systems.GetWorld()
                        .GetPool<IsPlaySoundComponent>()
                        .Add(entity);
                    isHitSoundComponent.SoundType = SoundsEnumType.GameOver;
                    //StopMusic
                    Extensions.AddPool<IsStopMusicComponent>(systems, entity);
                }
            }
        }
    }
}