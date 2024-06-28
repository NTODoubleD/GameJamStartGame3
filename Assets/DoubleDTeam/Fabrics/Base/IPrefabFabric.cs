using DoubleDTeam.Containers.Base;
using UnityEngine;

namespace DoubleDTeam.Fabrics.Base
{
    public interface IPrefabFabric : IModule
    {
        public GameObject Create(GameObject obj);
        public GameObject Create(GameObject obj, Vector3 position, Quaternion rotation, Transform parent);

        public TObject Create<TObject>(TObject obj)
            where TObject : MonoBehaviour;

        public TObject Create<TObject>(TObject obj, Vector3 position, Quaternion rotation, Transform parent)
            where TObject : MonoBehaviour;

        public void Return(GameObject obj);

        public void Return<TObject>(TObject obj) where TObject : MonoBehaviour;
    }
}