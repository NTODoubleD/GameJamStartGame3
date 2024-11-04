using System;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Buildings;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using Game.Gameplay.Sleigh;
using UnityEngine;

namespace Game.Gameplay.Deers
{
    public class DeerBornController
    {
        private readonly DayCycleController _dayCycleController;
        private readonly SleighSendController _sleighSendController;
        private readonly PastureBuilding _pastureBuilding;
        private readonly Herd _herd;
        private readonly DeerFabric _fabric;

        private readonly Stack<Tuple<Deer, Deer>> _pairs = new();
        private List<Deer> _suitableDeerRemains;

        public event Action DeerIsBorn;
        
        public DeerBornController(Herd herd, DeerFabric fabric, 
            DayCycleController dayCycleController, SleighSendController sleighSendController,
            BuildingsLocator buildingsLocator)
        {
            _herd = herd;
            _fabric = fabric;
            _dayCycleController = dayCycleController;
            _sleighSendController = sleighSendController;
            _pastureBuilding = buildingsLocator.PastureBuilding;
            
            _dayCycleController.DayStarted += OnDayStarted;
            _dayCycleController.DayEnded += OnDayEnded;
            _sleighSendController.SleighStarted += OnSleighStarted;
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
            var currentHerdCount = _herd.CurrentHerd.Count();
            var capacity = _pastureBuilding.GetDeerCapacity();

            var youngDeerAmount = Math.Clamp(capacity - currentHerdCount, 0, _pairs.Count);

            if (youngDeerAmount <= 0)
                return;

            Debug.Log("New deers: " + youngDeerAmount);

            for (int i = 0; i < youngDeerAmount; i++)
                _fabric.CreateDeer();

            SoundsManager.Instance.PlayNewbornOlen(Camera.main!.transform.position);
            DeerIsBorn?.Invoke();
        }

        private void OnDayEnded()
        {
            _suitableDeerRemains = _herd.CurrentHerd
                .Where(d => d.DeerInfo.Age != DeerAge.Young)
                .Where(d => d.DeerInfo.Status == DeerStatus.Standard).ToList();

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
        
        ~DeerBornController()
        {
            _dayCycleController.DayStarted -= OnDayStarted;
            _dayCycleController.DayEnded -= OnDayEnded;

            _sleighSendController.SleighStarted -= OnSleighStarted;
        }
    }
}