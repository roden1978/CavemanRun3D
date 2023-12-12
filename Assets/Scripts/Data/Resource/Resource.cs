using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HalfDiggers.Runner
{
    [Serializable]
    public struct Resource
    {
        public string ID;
        public string Name;
        public ResourceType Type;
        public AssetReferenceSprite Sprite;
        public ScriptableObject AdditionalData;
    }

    public enum ResourceType
    {
    }
}