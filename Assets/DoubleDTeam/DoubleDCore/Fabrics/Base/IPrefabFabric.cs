using UnityEngine;
using Zenject;

namespace DoubleDCore.Fabrics.Base
{
    public interface IPrefabFabric : IFactory<GameObject, GameObject>,
        IFactory<GameObject, Vector3, Quaternion, Transform, GameObject>
    {
        public TObject Create<TObject>(TObject prefab)
            where TObject : MonoBehaviour;

        public TObject Create<TObject>(TObject prefab, Vector3 position, Quaternion rotation, Transform parent)
            where TObject : MonoBehaviour;

        public void Return(GameObject obj);

        public void Return<TObject>(TObject obj) where TObject : MonoBehaviour;
    }
}