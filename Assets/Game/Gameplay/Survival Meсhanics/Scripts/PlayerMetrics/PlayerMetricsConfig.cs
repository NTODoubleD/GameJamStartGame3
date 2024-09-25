using UnityEngine;

namespace Game.Gameplay.Survival_Metrics.Configs
{
    [CreateAssetMenu(fileName = "Player Metrics Config", menuName = "Configs/PlayerMetrics")]
    public class PlayerMetricsConfig : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _heatResistance;
        [SerializeField] private float _hunger;
        [SerializeField] private float _thirst;
        [SerializeField] private float _endurance;
        
        public float Health => _health;
        public float HeatResistance => _heatResistance;
        public float Hunger => _hunger;
        public float Thirst => _thirst;
        public float Endurance => _endurance;
    }
}