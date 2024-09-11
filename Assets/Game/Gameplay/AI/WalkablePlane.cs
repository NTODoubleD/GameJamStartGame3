using DoubleDCore.Service;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Game.Gameplay.AI
{
    public class WalkablePlane : MonoService
    {
        [SerializeField] private float _width;

        private Vector3 Center => transform.position;

        public Vector3 GetRandomPointOnNavMesh()
        {
            Vector3 randomPoint = Center + new Vector3(Random.Range(-_width / 2, _width / 2), 0, 0) +
                                  new Vector3(0, 0, Random.Range(-_width / 2, _width / 2));

            return NavMesh.SamplePosition(randomPoint, out var hit, _width, NavMesh.AllAreas)
                ? hit.position
                : Center;
        }

        public void SetWidth(float width)
        {
            _width = width;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawCube(Center, new Vector3(_width, 5, _width));
        }
    }
}