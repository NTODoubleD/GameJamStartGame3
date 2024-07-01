using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;

namespace Game.Gameplay.Scripts
{
    public class Herd : MonoModule
    {
        private DeerFabric _fabric;

        private readonly List<Deer> _currentHerd = new();

        public IEnumerable<Deer> CurrentHerd => _currentHerd;

        private void Awake()
        {
            _fabric = Services.SceneContext.GetModule<DeerFabric>();
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
        }

        private void OnDied(Deer deer)
        {
            deer.Died -= OnDied;
            _currentHerd.Remove(deer);
        }
    }
}