using System;
using Game.Gameplay.Survival_Metrics.Configs;

namespace Game.Gameplay.SurvivalMechanics
{
    public class PlayerMetricsModel : IHeatResistable
    {
        private float _health;
        private float _heatResistance;
        private float _hunger;
        private float _thirst;
        private float _endurance;

        public PlayerMetricsModel(PlayerMetricsConfig config)
        {
            _health = config.Health;
            _heatResistance = config.HeatResistance;
            _hunger = config.Hunger;
            _thirst = config.Thirst;
            _endurance = config.Endurance;
        }
        
        public float Health
        {
            get => _health;
            set
            {
                if (value < 0)
                    _health = 0;
                else
                    _health = value;
                
                HealthChanged?.Invoke(_health);
            }
        }
        
        public float HeatResistance
        {
            get => _heatResistance;
            set
            {
                if (value < 0)
                    _heatResistance = 0;
                else
                    _heatResistance = value;
                
                HeatResistanceChanged?.Invoke(_heatResistance);
            }
        }
        
        public float Hunger
        {
            get => _hunger;
            set
            {
                if (value < 0)
                    _hunger = 0;
                else
                    _hunger = value;
                
                HungerChanged?.Invoke(_hunger);
            }
        }
        
        public float Thirst
        {
            get => _thirst;
            set
            {
                if (value < 0)
                    _thirst = 0;
                else
                    _thirst = value;
                
                ThirstChanged?.Invoke(_thirst);
            }
        }
        
        public float Endurance
        {
            get => _endurance;
            set
            {
                if (value < 0)
                    _endurance = 0;
                else
                    _endurance = value;
                
                EnduranceChanged?.Invoke(_endurance);
            }
        }

        public event Action<float> HealthChanged;
        public event Action<float> HeatResistanceChanged;
        public event Action<float> HungerChanged;
        public event Action<float> ThirstChanged;
        public event Action<float> EnduranceChanged;
    }
}