using Leopotam.EcsLite;
using UnityEngine;


namespace HalfDiggers.Runner
{


    public class DEBUG_TestDataManagerSystems : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<IsPlayerComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    systems.GetWorld().GetPool<IsManagePlayerStatsComponent>().Add(entity)
                        .dataAction = DataManageEnumType.Clear;
                }
            }
        }
    }
}