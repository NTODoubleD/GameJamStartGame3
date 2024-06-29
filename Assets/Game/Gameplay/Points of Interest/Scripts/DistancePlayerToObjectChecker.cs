using UnityEngine;

public class DistancePlayerToObjectChecker : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _rangeDistance;

    public bool IsPlayerInRange(Transform objectToCheck)
    {
        return Vector3.SqrMagnitude(_player.position - objectToCheck.position) <= _rangeDistance * _rangeDistance;
    }

    public float GetSqrDistanceToPlayer(Transform obj)
    {
        return Vector3.SqrMagnitude(obj.position - _player.position);
    }
}