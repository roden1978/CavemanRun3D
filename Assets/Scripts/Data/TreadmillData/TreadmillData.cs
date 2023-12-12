using UnityEngine;

namespace HalfDiggers.Runner
{
    [CreateAssetMenu(menuName = "Treadmill/TreadmillData", fileName = "New TreadmillData", order = 51)]
    public class TreadmillData : ScriptableObject
    {
        public float Speed;
        public float AccelerationInterval;
        public float AccelerationValue;
        public int StartPlatformCount;
        public GameObjectsTypeId UsingPlatform;
        public int PlatformsBeforePlayer => StartPlatformCount - 1;

    }
}