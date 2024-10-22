using System.Collections.Generic;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using Game.Gameplay.Configs;

namespace Game.Gameplay.Deers
{
    public class DeerAgeController
    {
        private const DeerAge DeathAge = DeerAge.None;
        
        private readonly HashSet<Deer> _deersToKill = new();
        private readonly DayCycleController _dayCycleController;
        private readonly Herd _herd;
        private readonly DeerAgeConfig _config;

        public DeerAgeController(DayCycleController dayCycleController, Herd herd, DeerAgeConfig config)
        {
            _dayCycleController = dayCycleController;
            _herd = herd;
            _config = config;
            
            _dayCycleController.DayStarted += AddAgeToDeer;
        }

        private void AddAgeToDeer()
        {
            _deersToKill.Clear();
            
            foreach (var deer in _herd.CurrentHerd)
            {
                var deerInfo = deer.DeerInfo;

                if (deerInfo.Status == DeerStatus.Killed)
                    continue;
                
                deerInfo.AgeDays++;

                if (_config.ReversedAgeTable.ContainsKey(deerInfo.AgeDays))
                {
                    DeerAge age = _config.ReversedAgeTable[deerInfo.AgeDays];

                    if (age == DeathAge)
                    {
                        _deersToKill.Add(deer);
                    }
                    else
                    {
                        deerInfo.Age = age;
                        deer.UpdateStateByAge();
                    }
                }
            }
            
            foreach (var deer in _deersToKill)
                deer.Die();
        }
        
        ~DeerAgeController()
        {
            _dayCycleController.DayStarted -= AddAgeToDeer;
        }
    }
}