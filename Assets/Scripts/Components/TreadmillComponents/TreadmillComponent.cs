using System.Collections.Generic;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public struct TreadmillComponent
    {
        public float Speed;
        public float AccelerationInterval;
        public float AccelerationValue;
        public int StartPlatformCount;
        public int PlatformsBeforePlayer;
        public Vector3 ReturnToPoolPoint;
        public Vector3 SpawnPlatformPoint;
        public float PlatformLength;
        public Queue<PlatformView> Platforms;
        public GameObjectsTypeId UsingPlatform;
    }
}