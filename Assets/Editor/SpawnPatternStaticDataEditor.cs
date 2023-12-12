using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameObjectsScripts;
using HalfDiggers.Runner;
using StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Editor
{
    [CustomEditor(typeof(SpawnPatternStaticData))]
    public class SpawnPatternStaticDataEditor : UnityEditor.Editor
    {
        private TunnelsDropDown _tunnelsDropDown;
        private int _index;

        private void Awake()
        {
            if (_tunnelsDropDown is null)
                LoadDropDown();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SpawnPatternStaticData spawnPatternData = (SpawnPatternStaticData)target;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            if (GUILayout.Button("Collect"))
            {
                spawnPatternData.ObjectSpawners = FindObjectsOfType<PickableObjectMarker>()
                    .Select(x =>
                        new PickableObjectSpawnerData(x.GetComponent<UniqueId>().Id,
                            $"{spawnPatternData.name}-{x.name}",
                            x.PickableObjectStaticData.GameObjectsTypeId, x.transform.position, x.transform.rotation))
                    .ToList();
            }

            if (GUILayout.Button("Clear pattern data"))
            {
                spawnPatternData.ObjectSpawners.Clear();
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Show markers on scene"))
            {
                foreach (PickableObjectSpawnerData marker in spawnPatternData.ObjectSpawners)
                {
                    LoadObject(marker);
                }
            }

            if (GUILayout.Button("Remove marker from scene"))
            {
                IEnumerable<UniqueId> markers = FindObjectsOfType<PickableObjectMarker>()
                    .Select(x => x.GetComponent<UniqueId>());
                foreach (UniqueId marker in markers)
                {
                    DestroyImmediate(marker.gameObject);
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            GUIContent tunnelsLabel = new("Tunnels");
            if(_tunnelsDropDown is not null)
                _index = EditorGUILayout.Popup(tunnelsLabel, _index, _tunnelsDropDown.Tunnels.ToArray());


            if (GUILayout.Button("Load Tunnels List"))
            {
                switch (spawnPatternData.PatternGroup)
                {
                    case PatternGroups.Ease:
                        LoadTunnelsList("EaseTunnel");
                        break;
                    case PatternGroups.Medium:
                        LoadTunnelsList("MediumTunnel");
                        break;
                    case PatternGroups.Hard:
                        LoadTunnelsList("HardTunnel");
                        break;
                    case PatternGroups.Lamp:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (GUILayout.Button("Instantiate tunnel prefab"))
            {
                if (FindObjectOfType<PlatformView>() is null)
                    LoadObject(spawnPatternData.TunnelPrefabName);
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Save tunnel type"))
            {
                spawnPatternData.TunnelPrefabName = _tunnelsDropDown.Tunnels[_index];
            }
            
            EditorGUILayout.Space();
            if (GUILayout.Button("Remove tunnels from scene"))
            {
                IEnumerable<PlatformView> platforms = FindAllPlatforms();
                foreach (PlatformView platform in platforms)
                {
                    DestroyImmediate(platform.gameObject);
                }
            }


            EditorUtility.SetDirty(target);
        }

        private static IEnumerable<PlatformView> FindAllPlatforms()
        {
            IEnumerable<PlatformView> platforms = FindObjectsOfType<PlatformView>(true);
            return platforms;
        }

        private async void LoadObject(PickableObjectSpawnerData marker)
        {
            string typeName = Enum.GetName(typeof(GameObjectsTypeId), marker.GameObjectsTypeId);
            Task<GameObject> result = Loading(typeName);
            await result;

            GameObject markerGo = Instantiate(result.Result, marker.LocalPosition, Quaternion.identity);
            markerGo.name = marker.Name;
        }

        private async void LoadObject(string objectName)
        {
            Task<GameObject> result = Loading(objectName);
            await result;
            Instantiate(result.Result, Vector3.zero, Quaternion.identity);
        }

        private async Task<GameObject> Loading(string objectName)
        {
            AsyncOperationHandle<MarkerData> soResult = Addressables.LoadAssetAsync<MarkerData>(objectName);
            await soResult.Task;

            AsyncOperationHandle<GameObject> goResult =
                Addressables.LoadAssetAsync<GameObject>(soResult.Result.MakerGameObject.RuntimeKey);
            await goResult.Task;

            return goResult.Result;
        }

        private async void LoadDropDown()
        {
            AsyncOperationHandle<TunnelsDropDown> soResult =
                Addressables.LoadAssetAsync<TunnelsDropDown>("TunnelsDropDown.asset");
            await soResult.Task;
            _tunnelsDropDown = soResult.Result;
        }

        private async void LoadTunnelsList(string label)
        {
            AsyncOperationHandle<IList<GameObject>> soResult =
                Addressables.LoadAssetsAsync<GameObject>(label, obj =>
                {
                    //Gets called for every loaded asset
                    Debug.Log(obj.name);
                });
            await soResult.Task;

            _tunnelsDropDown.Tunnels = soResult.Result.Select(x => x.name).ToList();
        }
    }
}