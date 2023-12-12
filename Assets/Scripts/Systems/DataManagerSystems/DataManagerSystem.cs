using Leopotam.EcsLite;
using System.IO;
using UnityEngine;


namespace HalfDiggers.Runner
{


    sealed class DataManagerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private PlayerSharedData _playerSharedData;
        private PlayerSaveData _playerSaveData;
        private EcsFilter _filter;
        private EcsPool<IsManagePlayerStatsComponent> _isManagePlayerStatsComponentPool;


        public void Init(IEcsSystems systems)
        {
            
            var world = systems.GetWorld();
            _filter = world.Filter<IsManagePlayerStatsComponent>().End();
            _isManagePlayerStatsComponentPool = world.GetPool<IsManagePlayerStatsComponent>();
            _playerSharedData = systems.GetShared<SharedData>().GetPlayerSharedData;
            LoadPlayerStats();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                switch (_isManagePlayerStatsComponentPool.Get(entity).dataAction)
                {
                    case DataManageEnumType.Save:
                        _playerSaveData.coins = _playerSharedData.GetPlayerCharacteristic.GetCurrentCoins;
                        SavePlayerStats();
                        break;
                    case DataManageEnumType.Load:
                        LoadPlayerStats();
                        break;
                    case DataManageEnumType.Clear:
                        ClearPlayerStats();
                        break;
                    default:
                        Debug.Log("Data manage Error");
                        break;
                }
                _isManagePlayerStatsComponentPool.Del(entity);
            }

        }

        private void SavePlayerStats()
        {
            string json = JsonUtility.ToJson(_playerSaveData);
            File.WriteAllText(Application.persistentDataPath + "/PlayerStats.json", json);
        }

        private void LoadPlayerStats()
        {
            string path = Application.persistentDataPath + "/PlayerStats.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _playerSaveData = JsonUtility.FromJson<PlayerSaveData>(json);
            }
            else
            {
                _playerSaveData = new PlayerSaveData();
            }
            _playerSharedData.GetPlayerCharacteristic.UpdateCoins(_playerSaveData.coins);
        }

        private void ClearPlayerStats()
        {
            _playerSaveData = new PlayerSaveData();
            _playerSharedData.GetPlayerCharacteristic.UpdateCoins(_playerSaveData.coins);
            SavePlayerStats();
        }
    }
}