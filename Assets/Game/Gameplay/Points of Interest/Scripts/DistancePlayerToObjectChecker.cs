using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public class DistancePlayerToObjectChecker : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        public bool IsPlayerInRange(InteractiveObject objectToCheck)
        {          
            return GetXZDistance(_player.position, objectToCheck.transform.position) <= objectToCheck.DistanceToInteract;
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