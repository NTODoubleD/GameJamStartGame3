using System;
using System.Collections.Generic;
using System.Linq;
using DoubleDTeam.Containers;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using Game.Gameplay.Sleigh;
using UnityEngine;

namespace Game.Gameplay.Deers
{
    public class DeerBornController : MonoBehaviour
    {
        [SerializeField] private DayCycleController _dayCycleController;
        [SerializeField] private SleighSendController _sleighSendController;

        private Herd _herd;
        private DeerFabric _fabric;

        private List<Deer> _suitableDeerRemains;
        private readonly Stack<Tuple<Deer, Deer>> _pairs = new();

        public event Action DeerIsBorn;

        private int YoungDeerAmount => _pairs.Count;

        private void Awake()
        {
            _herd = Services.SceneContext.GetModule<Herd>();
            _fabric = Services.SceneContext.GetModule<DeerFabric>();
        }

        private void OnEnable()
        {
            _dayCycleController.DayStarted += OnDayStarted;
            _dayCycleController.DayEnded += OnDayEnded;

            _sleighSendController.SleighStarted += OnSleighStarted;
        }

        private void OnDisable()
        {
            _dayCycleController.DayStarted -= OnDayStarted;
            _dayCycleController.DayEnded -= OnDayEnded;

            _sleighSendController.SleighStarted -= OnSleighStarted;
        }

        private void OnSleighStarted(int deerAmount)
        {
            for (int i = 0; i < deerAmount; i++)
            {
                if (_suitableDeerRemains.Count >= 0)
                {
                    _suitableDeerRemains.RemoveAt(0);
                    continue;
                }

                var pair = _pairs.Pop();

                _suitableDeerRemains.Add(pair.Item2);
            }
        }

        private void OnDayStarted()
        {
            Debug.Log("New deers: " + YoungDeerAmount);

            for (int i = 0; i < YoungDeerAmount; i++)
                _fabric.CreateDeer();

            DeerIsBorn?.Invoke();
        }

        private void OnDayEnded()
        {
            _suitableDeerRemains = _herd.CurrentHerd
                .Where(d => d.DeerInfo.Age == DeerAge.Adult)
                .Where(d => d.DeerInfo.Status == DeerStatus.Standard)
                .ToList();

            var males = _suitableDeerRemains.Where(d => d.DeerInfo.Gender == GenderType.Male).ToList();
            var females = _suitableDeerRemains.Where(d => d.DeerInfo.Gender == GenderType.Female).ToList();

            int minCount = Math.Min(males.Count, females.Count);

            _pairs.Clear();

            for (int i = 0; i < minCount; i++)
            {
                _pairs.Push(new Tuple<Deer, Deer>(males[i], females[i]));

                _suitableDeerRemains.Remove(males[i]);
                _suitableDeerRemains.Remove(females[i]);
            }
        }
    }
}