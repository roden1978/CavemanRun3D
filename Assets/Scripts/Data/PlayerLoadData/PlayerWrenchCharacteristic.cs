using System;
using Leopotam.EcsLite;
using UnityEngine;


namespace HalfDiggers.Runner
{
    [Serializable]
    public sealed class PlayerWrenchCharacteristic:IDisposable
    {
        public Action<IEcsSystems> IsLivesUpdate;
        [SerializeField] private int _baseWrench;
        [SerializeField] private int _maximumWrench;
        [SerializeField] private int _currentWrench;

        public int GetBaseWrench => _baseWrench;
        public int GetCurrentWrench => _currentWrench;


        public PlayerWrenchCharacteristic(PlayerWrenchCharacteristic playerCharacteristic)
        {
            _maximumWrench = playerCharacteristic._maximumWrench;
            _baseWrench = playerCharacteristic._baseWrench;
        }

        internal void LoadInitValue()
        {
            _currentWrench = _baseWrench;
        }

        internal int UpdateWrench(int value, ref int currentLives, int maxLives)
        {
            if (_currentWrench < _maximumWrench - 1)
            {
                _currentWrench += value;
            }
            else if(currentLives!=maxLives)
            {
                LoadInitValue();
               var sys = GameObject.FindObjectOfType<EcsStartup>().Systems;
                IsLivesUpdate?.Invoke(sys);
                return currentLives < maxLives ? currentLives = maxLives : currentLives;
            }
            else
            {
                LoadInitValue();
            }

            return _currentWrench;
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}