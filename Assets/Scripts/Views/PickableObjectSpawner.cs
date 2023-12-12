using UnityEngine;

namespace HalfDiggers.Runner
{
    public class PickableObjectSpawner : MonoBehaviour
    {
        //private bool _pickedUp;
        private string _id;
        private GameObjectsTypeId _pickableObjectTypeId;
        private IPoolService _poolService;
        private Transform _parent;
        private Vector3 _position;
        private float _chanceOfStaying = 0.5f;
        private Quaternion _rotation;

        public void Construct(string spawnerId, GameObjectsTypeId pickableObjectTypeId,
            IPoolService poolService, Transform parent, Vector3 position, Quaternion rotation)
        {
            _id = spawnerId;
            _pickableObjectTypeId = pickableObjectTypeId;
            _poolService = poolService;
            _parent = parent;
            _position = position;
            _rotation = rotation;
        }

        public GameObject Spawn()
        {
            GameObject pickableObject = default;
            
            if (GetChance())
            {
                pickableObject = _poolService.Get(_pickableObjectTypeId);
                pickableObject.transform.parent = _parent;
                pickableObject.transform.localPosition = _position;
                pickableObject.transform.rotation = _rotation;
            }

            return pickableObject;
        }

        private bool GetChance()
        {
            // if (_pickableObjectTypeId == GameObjectsTypeId.PillarLamp)
            //     return Random.value < _chanceOfStaying;
        
            return true;
        }

        /*private void OnPickUp()
        {
            if (_pickableObject != null)
                _pickableObject.PickUp -= OnPickUp;
            _pickedUp = true;
            Destroy(gameObject);
        }*/

        /*public void Enable()
        {
            if(!_pickedUp)
                _pickableObject.gameObject.SetActive(true);
        }

        public void Disable()
        {
            if(!_pickedUp)
                _pickableObject.gameObject.SetActive(false);
        }*/
    }
}