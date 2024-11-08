using System.Collections.Generic;
using DoubleDCore.Service;
using Game.Gameplay.SurvivalMechanics;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game.Gameplay.DayCycle
{
    public class DayCycleController : MonoService
    {
        [SerializeField] private DayChangeTransition _changeTransition;

        private GameInput _inputController;
        private IEnumerable<IRealtimeSurvivalMechanic> _survivalMechanics;

        public int CurrentDay { get; private set; } = 1;

        public event UnityAction DayEnded;
        public event UnityAction DayStarted;

        [Inject]
        private void Init(GameInput inputController, IEnumerable<IRealtimeSurvivalMechanic> survivalMechanics)
        {
            _inputController = inputController;
            _survivalMechanics = survivalMechanics;
        }

        public void EndDay()
        {
            _inputController.Disable();

            foreach (var mechanic in _survivalMechanics)
                mechanic.Disable();

            DayEnded?.Invoke();
            _changeTransition.Transit(StartDay);
        }

        public void StartDay()
        {
            _inputController.Enable();

            foreach (var mechanic in _survivalMechanics)
            {
                mechanic.Unpause();
                mechanic.Enable();
            }

            CurrentDay++;
            DayStarted?.Invoke();
        }
    }
}