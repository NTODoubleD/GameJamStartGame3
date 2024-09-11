using DoubleDCore.Fabrics.Base;
using UnityEngine;
using Zenject;

namespace DoubleDCore.Fabrics
{
    public class ZenjectPrefabFabric : IPrefabFabric
    {
        private readonly DiContainer _container;

        public ZenjectPrefabFabric(DiContainer container)
        {
            _container = container;
        }

        public GameObject Create(GameObject prefab)
        {
            return _container.InstantiatePrefab(prefab, Vector3.zero, Quaternion.identity, null);
        }

        public GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            return _container.InstantiatePrefab(prefab, position, rotation, parent);
        }

        public TObject Create<TObject>(TObject prefab) where TObject : MonoBehaviour
        {
            var inst = _container.InstantiatePrefab(prefab, Vector3.zero, Quaternion.identity, null);

            return inst.GetComponent<TObject>();
        }

        public TObject Create<TObject>(TObject prefab, Vector3 position, Quaternion rotation, Transform parent)
            where TObject : MonoBehaviour
        {
            var inst = _container.InstantiatePrefab(prefab, position, rotation, parent);

            return inst.GetComponent<TObject>();
        }

        public void Return(GameObject obj)
        {
            Object.Destroy(obj.gameObject);
        }

        public void Return<TObject>(TObject obj) where TObject : MonoBehaviour
        {
            Object.Destroy(obj.gameObject);
        }
    }
}