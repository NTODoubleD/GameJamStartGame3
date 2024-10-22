using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Configs;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Deers
{
    public class DeerIllnessesController
    {
        private readonly Dictionary<Deer, int> _deersIllnesses = new();
        
        private readonly DayCycleController _dayCycleController;
        private readonly DeerHealController _healController;
        private readonly Herd _herd;
        private readonly DeerIllnessesConfig _config;

        public DeerIllnessesController(DayCycleController dayCycleController, DeerHealController healController,
            Herd herd, DeerIllnessesConfig config)
        {
            _dayCycleController = dayCycleController;
            _healController = healController;
            _herd = herd;
            _config = config;

            _dayCycleController.DayStarted += OnDayStarted;
            _healController.Healed += OnDeerHealed;
        }

        private void OnDayStarted()
        {
            ContinueIllnesses();
            CastIllnesses();
        }

        private void CastIllnesses()
        {
            List<Deer> possibleTargets = _herd.CurrentHerd.Where(deer => deer.DeerInfo.Age != DeerAge.Young).ToList();
            int targetsToCastIllnessCount = possibleTargets.Count >= _config.BigFlockCount
                ? _config.BigFlockIllnessesCount
                : _config.SmallFlockIllnessesCount;

            possibleTargets = possibleTargets.Where(deer => deer.DeerInfo.Status == DeerStatus.Standard).ToList();
            int sickedCount = 0;

            for (int i = 0; i < targetsToCastIllnessCount; i++)
            {
                if (sickedCount == targetsToCastIllnessCount || possibleTargets.Count == 0)
                    break;

                Deer deer = possibleTargets[Random.Range(0, possibleTargets.Count)];
                possibleTargets.Remove(deer);

                float castedChance = Random.Range(0f, 1f);

                if (castedChance <= _config.IllnessChance)
                {
                    deer.DeerInfo.Status = DeerStatus.Sick;
                    _deersIllnesses.Add(deer, 1);
                    sickedCount++;
                }
            }
        }

        private void ContinueIllnesses()
        {
            foreach (var deer in new List<Deer>(_deersIllnesses.Keys))
            {
                int currentDaysCount = _deersIllnesses[deer];
                currentDaysCount++;

                if (currentDaysCount > _config.EasySickDays)
                    deer.DeerInfo.Status = DeerStatus.VerySick;
                else if (currentDaysCount > _config.DeathSickDays)
                    deer.Die();
            }
        }

        private void OnDeerHealed(Deer deer)
        {
            _deersIllnesses.Remove(deer);
        }
        
        ~DeerIllnessesController()
        {
            _dayCycleController.DayStarted -= OnDayStarted;
            _healController.Healed -= OnDeerHealed;
        }
    }
}