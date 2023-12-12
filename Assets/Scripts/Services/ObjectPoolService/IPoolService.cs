﻿using System.Threading.Tasks;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public interface IPoolService
    {
        Task Initialize();
        GameObject Get(GameObjectsTypeId gameObjectsTypeId);
        void Add(GameObjectsTypeId type, GameObject pooledObject, int capacity);
        void Clear();
        void Clear(GameObjectsTypeId gameObjectsTypeId);
        void Return(GameObject gameObject);
        int Count { get; }
    }
}