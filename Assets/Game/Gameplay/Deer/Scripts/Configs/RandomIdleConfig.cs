using UnityEngine;

namespace Game.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "Deer Random Idle Config", menuName = "Configs/DeerRandomIdleConfig")]
    public class RandomIdleConfig : ScriptableObject
    {
        [SerializeField] private int _minimalSecondsToStay;
        [SerializeField] private int _maximumSecondsToStay;

        public int MinSeconds => _minimalSecondsToStay;
        public int MaxSeconds => _maximumSecondsToStay;
    }
}