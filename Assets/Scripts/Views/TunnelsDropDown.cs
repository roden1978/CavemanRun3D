using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "New TunnelsDropDown", menuName = "StaticData/TunnelsDropDown")]
    public class TunnelsDropDown : ScriptableObject
    {
        [HideInInspector]
        public List<string> Tunnels = new();
    }
}