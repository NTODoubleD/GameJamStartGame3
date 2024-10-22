using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using System.Collections.Generic;
using Game.Gameplay.Configs;
using UnityEngine;

namespace Game.Gameplay.Deers
{
    public class DeerHungerController
    {
        private readonly DayCycleController _dayCycleController;
        private readonly DeerHungerConfig _config;
        private readonly Herd _herd;
        private readonly HashSet<Deer> _deersToDie = new();

        public DeerHungerController(Herd herd, DayCycleController dayCycleController,
            DeerHungerConfig config)
        {
            _herd = herd;
            _dayCycleController = dayCycleController;
            _config = config;
            
            _dayCycleController.DayStarted += AddHungerToDeer;
        }

        private void AddHungerToDeer()
        {
            _deersToDie.Clear();

            foreach (var deer in _herd.CurrentHerd)
            {
                deer.DeerInfo.HungerDegree = Mathf.Max(0, deer.DeerInfo.HungerDegree - _config.HungerStep);

                if (deer.DeerInfo.HungerDegree < _config.MinimalHungerDegree)
                    _deersToDie.Add(deer);
            }

            foreach (var deer in _deersToDie)
                deer.Die();
        }
        
        ~DeerHungerController()
        {
            _dayCycleController.DayStarted -= AddHungerToDeer;
        }
    }
}