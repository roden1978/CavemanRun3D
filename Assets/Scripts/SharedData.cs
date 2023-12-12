using System.Threading.Tasks;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace HalfDiggers.Runner
{
    public sealed class SharedData
    {
        private ResourcesData _resourcesData;
        private PrefabsData _prefabsData;
        private PlayerSharedData _playerShared;
        private Transform _canvas;

        public ResourcesData GetResourcesData => _resourcesData;
        public PrefabsData GetPrefabsData => _prefabsData;
        public Transform GetCanvas => _canvas;

        public PlayerSharedData GetPlayerSharedData => _playerShared;

        public async Task Init()
        {
            //   var handleResources = Addressables.LoadAssetAsync<ResourcesData>(AssetsNamesConstants.RESOURCES_DATA_NAME);
            //  var handleMainCanvas =Addressables.LoadAssetAsync<GameObject>(AssetsNamesConstants.MAIN_CANVAS_PREFAB_NAME);
            AsyncOperationHandle<PlayerSharedData> handlePlayer =
                Addressables.LoadAssetAsync<PlayerSharedData>(AssetsNamesConstants.PLAYER_SHARED_DATA);
            await handlePlayer.Task;
            
            _playerShared = handlePlayer.Result;

            //  _resourcesData = handleResources.Result;

            //   _prefabsData = new PrefabsData();

            //  await _prefabsData.Init();

            //   CreateCanvas(handleMainCanvas.Result);

            //  Addressables.Release(handleMainCanvas);
        }

        private void CreateCanvas(GameObject prefab)
        {
            _canvas = Object.Instantiate(prefab).transform;
        }
    }
}