using System;
using UnityEngine;


namespace HalfDiggers.Runner
{


    [Serializable]
    public sealed class PlayerSaveData
    {
        public int coins;

        public PlayerSaveData()
        {
            coins = 0;
        }
    }
}