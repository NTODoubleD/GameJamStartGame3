using System;
using System.Collections.Generic;
using System.Linq;
using DoubleDCore.Service;
using Zenject;

namespace Game.Gameplay.Scripts
{
    public class Herd : MonoService
    {
        private DeerFabric _fabric;

        private readonly List<Deer> _currentHerd = new();

        public IEnumerable<Deer> CurrentHerd => _currentHerd;

        public List<Deer> SuitableDeer => CurrentHerd
            .Where(d => d.DeerInfo.Age == DeerAge.Adult)
            .Where(d => d.DeerInfo.Status == DeerStatus.Standard)
            .ToList();

        public event Action<int> HerdCountChanged;

        [Inject]
        private void Init(DeerFabric deerFabric)
        {
            _fabric = deerFabric;
        }

        private void OnEnable()
        {
            _fabric.Created += OnCreated;
        }

        private void OnDisable()
        {
            _fabric.Created -= OnCreated;
        }

        private void OnCreated(Deer deer)
        {
            _currentHerd.Add(deer);
            deer.Died += OnDied;
            
            HerdCountChanged?.Invoke(_currentHerd.Count);
        }

        private void OnDied(Deer deer)
        {
            deer.Died -= OnDied;
            _currentHerd.Remove(deer);
            
            HerdCountChanged?.Invoke(_currentHerd.Count);
        }
    }
}