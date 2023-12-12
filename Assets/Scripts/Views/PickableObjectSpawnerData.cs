using System;
using HalfDiggers.Runner;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class PickableObjectSpawnerData
    {
        public string Id;
        public string Name;
        public GameObjectsTypeId GameObjectsTypeId;
        public Vector3 LocalPosition;
        public Quaternion Rotation;

        public PickableObjectSpawnerData(string id, string name, GameObjectsTypeId gameObjectsTypeId, Vector3 localPosition, Quaternion rotation)
        {
            Id = id;
            Name = name;
            GameObjectsTypeId = gameObjectsTypeId;
            LocalPosition = localPosition;
            Rotation = rotation;
        }
    }
}