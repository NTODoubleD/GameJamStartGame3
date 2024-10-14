using UnityEngine;

namespace Game.Gameplay.Survival_Metrics.Configs
{
    [CreateAssetMenu(fileName = "Player Metrics Config", menuName = "Configs/PlayerMetrics")]
    public class PlayerMetricsConfig : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _heatResistance;
        [SerializeField] private float _hunger;
        [SerializeField] private float _endurance;
        [SerializeField] private float _maxValue;
        
        public float Health => _health;
        public float HeatResistance => _heatResistance;
        public float Hunger => _hunger;
        public float Endurance => _endurance;
        public float MaxValue => _maxValue;
    }
}