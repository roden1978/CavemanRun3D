using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;



namespace HalfDiggers.Runner
{
    public sealed class PrefabsData
    {
        public GameObject SomeObj;
        public async Task Init()
        {
            var somePrefab =
                Addressables.LoadAssetAsync<GameObject>(AssetsNamesConstants.WINDOW_PREFAB_NAME);
            
            await Task.WhenAll(somePrefab.Task);
            SomeObj = somePrefab.Result.GetComponent<GameObject>();
          
        }
    }
}