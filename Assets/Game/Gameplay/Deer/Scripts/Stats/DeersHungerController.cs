using Game.Gameplay;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Deers
{
    public class DeersHungerController : MonoBehaviour
    {
        private readonly HashSet<Deer> _currentDeers = new();

        [SerializeField] private DeerFabric _deerFabric;
        [SerializeField] private DayCycleController _dayCycleController;
        [SerializeField] private float _hungerStep = 0.2f;
        [SerializeField] private float _minimalHungerDegree = 0.4f;

        private void OnEnable()
        {
            _deerFabric.Created += OnDeerCreated;
            _dayCycleController.DayStarted += HungerDeers;
        }

        private void OnDisable()
        {
            _deerFabric.Created -= OnDeerCreated;
            _dayCycleController.DayStarted -= HungerDeers;
        }

        private void OnDeerCreated(Deer deer)
        {
            _currentDeers.Add(deer);
            deer.Died += OnDeerDied;
        }

        private void OnDeerDied(Deer deer)
        {
            deer.Died -= OnDeerDied;
            _currentDeers.Remove(deer);
        }

        private void HungerDeers()
        {
            List<Deer> deersToDie = new List<Deer>();

            foreach (var deer in _currentDeers)
            {
                deer.DeerInfo.HungerDegree = Mathf.Max(0, deer.DeerInfo.HungerDegree - _hungerStep);

                if (deer.DeerInfo.HungerDegree < _minimalHungerDegree)
                    deersToDie.Add(deer);
            }

            foreach (var deer in deersToDie)
                deer.Die();
        }
    }
}