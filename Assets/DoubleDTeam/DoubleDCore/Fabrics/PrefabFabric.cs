using DoubleDCore.Fabrics.Base;
using UnityEngine;

namespace DoubleDCore.Fabrics
{
    public class PrefabFabric : IPrefabFabric
    {
        public GameObject Create(GameObject obj)
        {
            return Object.Instantiate(obj);
        }

        public GameObject Create(GameObject obj, Vector3 position, Quaternion rotation, Transform parent)
        {
            return Object.Instantiate(obj, position, rotation, parent);
        }

        public TObject Create<TObject>(TObject obj) where TObject : MonoBehaviour
        {
            return Object.Instantiate(obj);
        }

        public TObject Create<TObject>(TObject obj, Vector3 position, Quaternion rotation, Transform parent)
            where TObject : MonoBehaviour
        {
            return Object.Instantiate(obj, position, rotation, parent);
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