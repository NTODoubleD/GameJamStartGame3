using System;
using Game.Gameplay.Survival_Metrics.Configs;

namespace Game.Gameplay.SurvivalMechanics
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
                if (value < 0)
                    _health = 0;
                else
                    _health = value;
                
                HealthChanged?.Invoke(_health);
            }
        }
        
        public int HeatResistance
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
        
        public int Hunger
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
        
        public int Thirst
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
        
        public int Endurance
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

        public event Action<int> HealthChanged;
        public event Action<int> HeatResistanceChanged;
        public event Action<int> HungerChanged;
        public event Action<int> ThirstChanged;
        public event Action<int> EnduranceChanged;
    }
}