using System.Collections.Generic;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using Game.Gameplay.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Deers
{
    public class DeerAgeController : MonoBehaviour
    {
        private const DeerAge DeathAge = DeerAge.None;
        
        private readonly HashSet<Deer> _deersToKill = new();
        
        private DayCycleController _dayCycleController;
        private Herd _herd;
        private DeerAgeConfig _config;

        [Inject]
        private void Init(DayCycleController dayCycleController, Herd herd, DeerAgeConfig config)
        {
            _dayCycleController = dayCycleController;
            _herd = herd;
            _config = config;
        }

        private void OnEnable()
        {
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

        private void OnDisable()
        {
            _dayCycleController.DayStarted -= AddAgeToDeer;
        }
    }
}