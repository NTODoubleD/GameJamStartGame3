using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public class DistancePlayerToObjectChecker : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _rangeDistance;

        public bool IsPlayerInRange(Transform objectToCheck)
        {
            return Vector3.Distance(_player.position, objectToCheck.position) <= _rangeDistance;
        }

        public float GetDistanceToPlayer(Transform obj)
        {
            return Vector3.Distance(obj.position, _player.position);
        }
    }
}