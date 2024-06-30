using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Scripts
{
    public class DeerMeshing : MonoBehaviour
    {
        [SerializeField] private Vector3 _normalScale;
        [SerializeField] private Vector3 _youngScale;

        [SerializeField] private SkinnedMeshRenderer _meshRenderer;

        [Space, SerializeField] private List<DeerMeshingInfo> _maleMeshingInfo;
        [SerializeField] private List<DeerMeshingInfo> _femaleMeshingInfo;

        private Dictionary<GenderType, Dictionary<DeerAge, DeerMeshingInfo>> _meshingTable;

        private void Awake()
        {
            _meshingTable = new();

            _meshingTable.Add(GenderType.Male, new Dictionary<DeerAge, DeerMeshingInfo>());
            foreach (var meshingInfo in _maleMeshingInfo)
                _meshingTable[GenderType.Male].Add(meshingInfo.Age, meshingInfo);

            _meshingTable.Add(GenderType.Female, new Dictionary<DeerAge, DeerMeshingInfo>());
            foreach (var meshingInfo in _femaleMeshingInfo)
                _meshingTable[GenderType.Female].Add(meshingInfo.Age, meshingInfo);
        }

        public void ChangeMesh(DeerAge deerAge, GenderType genderType)
        {
            transform.localScale = deerAge == DeerAge.Young ? _youngScale : _normalScale;
        }

        private void DisableAllOutlines()
        {
            foreach (var (_, dictionary) in _meshingTable)
            {
                foreach (var (_, meshingInfo) in dictionary)
                {
                    meshingInfo.Outline.SetActive(false);
                }
            }
        }
    }

    [Serializable]
    public class DeerMeshingInfo
    {
        public DeerAge Age;
        public Mesh Mesh;
        public GameObject Outline;
    }
}