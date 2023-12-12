using System.Collections.Generic;
using UnityEngine;

namespace HalfDiggers.Runner
{
    public struct IsPlatformComponent
    {
        public List<GameObject> PillarLamps;
        public List<GameObject> WallLamps;
        public List<GameObject> PickableObjects;

        public void Clear()
        {
            PillarLamps.Clear();
            WallLamps.Clear();
            PickableObjects.Clear();
        }
    }
}