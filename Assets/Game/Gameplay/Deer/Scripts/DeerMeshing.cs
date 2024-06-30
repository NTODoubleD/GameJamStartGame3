using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Scripts
{
    public class DeerMeshing : MonoBehaviour
    {
        public Dictionary<DeerAge, DeerMeshingInfo> _maleMeshingInfo;
        public Dictionary<DeerAge, DeerMeshingInfo> _femaleMeshingInfo;
    }

    [Serializable]
    public class DeerMeshingInfo
    {
        public Mesh Mesh;
        public GameObject Outline;
    }

    // public class MeshingDictionary : UnitySerializedDictionary<DeerAge, DeerMeshingInfo>
    // {
    // }
}