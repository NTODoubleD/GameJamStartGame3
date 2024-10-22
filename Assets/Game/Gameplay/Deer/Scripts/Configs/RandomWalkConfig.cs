using UnityEngine;

namespace Game.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "Deer Random Walk Config", menuName = "Configs/DeerRandomWalkConfig")]
    public class RandomWalkConfig : ScriptableObject
    {
        [SerializeField] private float _distanceAccuracy = .1f;
        [SerializeField] private float _maxWalkDuration = 13;

        public float DistanceAccuracy => _distanceAccuracy;
        public float MaxWalkDuration => _maxWalkDuration;
    }
}