using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public class DistancePlayerToObjectChecker : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _rangeDistance;

        public bool IsPlayerInRange(Transform objectToCheck)
        {          
            return GetXZDistance(_player.position, objectToCheck.position) <= _rangeDistance;
        }

        public float GetDistanceToPlayer(Transform obj)
        {
            return GetXZDistance(_player.position, obj.position);
        }

        private float GetXZDistance(Vector3 obj1Position, Vector3 obj2Position)
        {
            var xzObj1Position = new Vector3(obj1Position.x, 0, obj1Position.z);
            var xzObj2Position = new Vector3(obj2Position.x, 0, obj2Position.z);
            return Vector3.Distance(xzObj1Position, xzObj2Position);
        }
    }
}