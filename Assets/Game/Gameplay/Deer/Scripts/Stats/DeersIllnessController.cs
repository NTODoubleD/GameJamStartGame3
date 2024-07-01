using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay.Deers
{
    public class DeersIllnessController : MonoBehaviour
    {
        private readonly HashSet<Deer> _currentDeers = new();
        private readonly Dictionary<Deer, int> _deersIllnesses = new();

        [SerializeField] private DeerFabric _deerFabric;
        [SerializeField] private DayCycleController _dayCycleController;
        [SerializeField] private DeerHealController _healController;

        [Header("Cast Ill Settings")]
        [SerializeField] private float _illnessChance = 0.2f;
        [SerializeField] private int _bigFlockIllnessesCount = 2;
        [SerializeField] private int _smallFlockIllnessesCount = 1;
        [SerializeField] private int _bigFlockCount = 4;

        [Header("Continue Ill Settings")]
        [SerializeField] private int _easySickDays = 1;
        [SerializeField] private int _deathSickDays = 3;

        private void OnEnable()
        {
            _deerFabric.Created += OnDeerCreated;
            _dayCycleController.DayStarted += OnDayStarted;
            _healController.Healed += OnDeerHealed;
        }

        private void OnDisable()
        {
            _deerFabric.Created -= OnDeerCreated;
            _dayCycleController.DayStarted -= OnDayStarted;
            _healController.Healed -= OnDeerHealed;
        }

        private void OnDeerCreated(Deer deer)
        {
            _currentDeers.Add(deer);
            deer.Died += OnDeerDied;
        }

        private void OnDayStarted()
        {
            ContinueIllnesses();
            CastIllnesses();
        }

        private void CastIllnesses()
        {
            List<Deer> possibleTargets = _currentDeers.Where(deer => deer.DeerInfo.Age != DeerAge.Young).ToList();
            int targetsToCastIllnessCount = possibleTargets.Count >= _bigFlockCount ? _bigFlockIllnessesCount : _smallFlockIllnessesCount;

            possibleTargets = possibleTargets.Where(deer => deer.DeerInfo.Status == DeerStatus.Standard).ToList();
            int sickedCount = 0;

            for (int i = 0; i < targetsToCastIllnessCount; i++)
            {
                if (sickedCount == targetsToCastIllnessCount || possibleTargets.Count == 0)
                    break;

                Deer deer = possibleTargets[Random.Range(0, possibleTargets.Count)];
                possibleTargets.Remove(deer);

                float castedChance = Random.Range(0f, 1f);

                if (castedChance <= _illnessChance)
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

                if (currentDaysCount > _easySickDays)
                    deer.DeerInfo.Status = DeerStatus.VerySick;
                else if (currentDaysCount > _deathSickDays)
                    deer.Die();
            }
        }

        private void OnDeerDied(Deer deer)
        {
            deer.Died -= OnDeerDied;
            _deersIllnesses.Remove(deer);
            _currentDeers.Remove(deer);
        }

        private void OnDeerHealed(Deer deer)
        {
            _deersIllnesses.Remove(deer);
        }
    }
}