using System.Collections.Generic;
using HalfDiggers.Runner;
using UnityEngine;
using UnityEngine.Serialization;

namespace StaticData
{
    [CreateAssetMenu(fileName = "New SpawnPattern", menuName = "StaticData/SpawnPatternData")]
    public class SpawnPatternStaticData : ScriptableObject
    {
        [FormerlySerializedAs("Difficult")] public PatternGroups PatternGroup;
        public List<PickableObjectSpawnerData> ObjectSpawners;
        public string TunnelPrefabName;
    }
}