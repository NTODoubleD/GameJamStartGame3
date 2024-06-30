using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Game.Gameplay.AI
{
    public class WalkablePlane : MonoBehaviour
    {
        [SerializeField] private float _radius;

        private Vector3 Center => transform.position;

        public Vector3 GetRandomPointOnNavMesh()
        {
            Vector3 randomPoint = Center + Random.insideUnitSphere * _radius;

            return NavMesh.SamplePosition(randomPoint, out var hit, _radius, NavMesh.AllAreas)
                ? hit.position
                : Center;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(Center, _radius);
        }
    }
}