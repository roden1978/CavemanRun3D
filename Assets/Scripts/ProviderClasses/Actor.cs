﻿using UnityEngine;

namespace HalfDiggers.Runner
{
    public abstract class Actor : MonoBehaviour,IActor
    {
        public int Entity => _entityIndex;
        private int _entityIndex;
        public abstract void Handle();

        public void AddEntity(int entity)
        {
            _entityIndex = entity;
        }
    }
}