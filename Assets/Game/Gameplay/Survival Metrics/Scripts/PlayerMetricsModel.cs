using System;
using Game.Gameplay.Survival_Metrics.Configs;
using UnityEngine;

namespace Game.Gameplay.Survival_Metrics
{
    public class PlayerMetricsModel
    {
        private int _health;
        private int _heatResistance;
        private int _hunger;
        private int _thirst;
        private int _endurance;

        public PlayerMetricsModel(PlayerMetricsConfig config)
        {
            _health = config.Health;
            _heatResistance = config.HeatResistance;
            _hunger = config.Hunger;
            _thirst = config.Thirst;
            _endurance = config.Endurance;
        }
        
        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                HealthChanged?.Invoke(_health);
            }
        }
        
        public int HeatResistance
        {
            get => _heatResistance;
            set
            {
                _heatResistance = value;
                HeatResistanceChanged?.Invoke(_heatResistance);
            }
        }
        
        public int Hunger
        {
            get => _hunger;
            set
            {
                _hunger = value;
                HungerChanged?.Invoke(_hunger);
            }
        }
        
        public int Thirst
        {
            get => _thirst;
            set
            {
                _thirst = value;
                ThirstChanged?.Invoke(_thirst);
            }
        }
        
        public int Endurance
        {
            get => _endurance;
            set
            {
                _endurance = value;
                EnduranceChanged?.Invoke(_endurance);
            }
        }

        public event Action<int> HealthChanged;
        public event Action<int> HeatResistanceChanged;
        public event Action<int> HungerChanged;
        public event Action<int> ThirstChanged;
        public event Action<int> EnduranceChanged;
    }
}