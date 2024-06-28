
using DoubleDTeam.Attributes;
using UnityEditor;
using UnityEngine;

namespace DoubleDTeam.Optimization
{
    [RequireComponent(typeof(MeshRenderer))]
    public class GpuInstancingEnabler : MonoBehaviour
    {
        [ReadOnlyProperty, SerializeField] private MeshRenderer _meshRenderer;

        private void Awake() =>
            _meshRenderer.SetPropertyBlock(new MaterialPropertyBlock());
        
        private void OnValidate()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
    }
}