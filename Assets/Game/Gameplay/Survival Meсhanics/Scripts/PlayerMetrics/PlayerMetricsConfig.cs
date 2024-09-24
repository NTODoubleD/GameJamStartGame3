using UnityEngine;

namespace Game.Gameplay.Survival_Metrics.Configs
{
    [CreateAssetMenu(fileName = "Player Metrics Config", menuName = "Configs/PlayerMetrics")]
    public class PlayerMetricsConfig : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _heatResistance;
        [SerializeField] private int _hunger;
        [SerializeField] private int _thirst;
        [SerializeField] private int _endurance;
        
        public int Health => _health;
        public int HeatResistance => _heatResistance;
        public int Hunger => _hunger;
        public int Thirst => _thirst;
        public int Endurance => _endurance;
    }
}