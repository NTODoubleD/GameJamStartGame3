using UnityEngine;

namespace Game.Gameplay.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Deer Random Walk Config", menuName = "Configs/DeerRandomWalkConfig")]
    public class RandomWalkConfig : ScriptableObject
    {
        [SerializeField] private float _distanceAccuracy = .1f;

        public float DistanceAccuracy => _distanceAccuracy;
    }
}