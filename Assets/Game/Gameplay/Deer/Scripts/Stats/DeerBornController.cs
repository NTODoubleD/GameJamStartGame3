using System;
using System.Collections.Generic;
using System.Linq;
using DoubleDTeam.Containers;
using Game.Gameplay.Buildings;
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

        [Space, SerializeField] private PastureBuilding _pastureBuilding;

        private Herd _herd;
        private DeerFabric _fabric;

        private List<Deer> _suitableDeerRemains;
        private readonly Stack<Tuple<Deer, Deer>> _pairs = new();

        private Dictionary<int, int> BuildLevelToCapacityTable = new()
        {
            { 1, 3 },
            { 2, 4 },
            { 3, 5 }
        };

        public event Action DeerIsBorn;

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
                if (_suitableDeerRemains.Count > 0)
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
            var currentYoung = _herd.CurrentHerd.Count(d => d.DeerInfo.Age == DeerAge.Young);
            var capacity = BuildLevelToCapacityTable[_pastureBuilding.CurrentLevel];

            var youngDeerAmount = Math.Clamp(capacity - currentYoung, 0, _pairs.Count);

            Debug.Log("New deers: " + youngDeerAmount);

            for (int i = 0; i < youngDeerAmount; i++)
                _fabric.CreateDeer();

            DeerIsBorn?.Invoke();
        }

        private void OnDayEnded()
        {
            _suitableDeerRemains = _herd.SuitableDeer;

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