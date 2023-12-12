using Leopotam.EcsLite;
using LeopotamGroup.Globals;

namespace HalfDiggers.Runner
{
    public class AccelerationPlatformSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _treadmillFilter;
        private EcsPool<TreadmillComponent> _treadmillComponentPool;
        private float _duration;
        private ITimeService _timeService;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _treadmillFilter = world.Filter<IsTreadmillComponent>().Inc<TreadmillComponent>().End();
            _treadmillComponentPool = world.GetPool<TreadmillComponent>();
            _timeService = Service<ITimeService>.Get();
        }

        public void Run(IEcsSystems systems)
        {
            ref TreadmillComponent treadmillComponent =
                ref _treadmillComponentPool.Get(_treadmillFilter.GetRawEntities()[0]);

            if (_duration < treadmillComponent.AccelerationInterval)
            {
                _duration += _timeService.DeltaTime;
            }
            else
            {
                treadmillComponent.Speed += treadmillComponent.AccelerationValue;
                _duration = 0;
            }
        }
    }
}