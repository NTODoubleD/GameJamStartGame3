using System;
using Game.Gameplay.Survival_Metrics.Configs;
using UnityEngine;

namespace Game.Gameplay.SurvivalMechanics
{
    public class PlayerMetricsModel : IHeatResistable
    {
        public readonly float MaxValue;
        
        private float _health;
        private float _heatResistance;
        private float _hunger;
        private float _endurance;

        public PlayerMetricsModel(PlayerMetricsConfig config)
        {
            _health = config.Health;
            _heatResistance = config.HeatResistance;
            _hunger = config.Hunger;
            _endurance = config.Endurance;
            MaxValue = config.MaxValue;
        }
        
        public float Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0, MaxValue);
                HealthChanged?.Invoke(_health);
            }
        }
        
        public float HeatResistance
        {
            get => _heatResistance;
            set
            {
                _heatResistance = Mathf.Clamp(value, 0, MaxValue);
                HeatResistanceChanged?.Invoke(_heatResistance);
            }
        }
        
        public float Hunger
        {
            get => _hunger;
            set
            {
                _hunger = Mathf.Clamp(value, 0, MaxValue);
                HungerChanged?.Invoke(_hunger);
            }
        }
        
        public float Endurance
        {
            get => _endurance;
            set
            {
                _endurance = Mathf.Clamp(value, 0, MaxValue);
                EnduranceChanged?.Invoke(_endurance);
            }
        }

        public event Action<float> HealthChanged;
        public event Action<float> HeatResistanceChanged;
        public event Action<float> HungerChanged;
        public event Action<float> EnduranceChanged;
    }
}