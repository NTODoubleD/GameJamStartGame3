using System;
using Game.Gameplay;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using System.Collections.Generic;
using DoubleDTeam.Containers;
using UnityEngine;

namespace Game.Gameplay.Deers
{
    public class DeersHungerController : MonoBehaviour
    {
        [SerializeField] private DeerFabric _deerFabric;
        [SerializeField] private DayCycleController _dayCycleController;
        [SerializeField] private float _hungerStep = 0.2f;
        [SerializeField] private float _minimalHungerDegree = 0.4f;

        private Herd _herd;

        private void Awake()
        {
            _herd = Services.SceneContext.GetModule<Herd>();
        }

        private void OnEnable()
        {
            _dayCycleController.DayStarted += HungerDeers;
        }

        private void OnDisable()
        {
            _dayCycleController.DayStarted -= HungerDeers;
        }

        private void HungerDeers()
        {
            List<Deer> deersToDie = new List<Deer>();

            foreach (var deer in _herd.CurrentHerd)
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