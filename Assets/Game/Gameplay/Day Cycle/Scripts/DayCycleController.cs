using DoubleDCore.Service;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game.Gameplay.DayCycle
{
    public class DayCycleController : MonoService
    {
        [SerializeField] private DayChangeTransition _changeTransition;

        private GameInput _inputController;

        public int CurrentDay { get; private set; } = 1;

        public event UnityAction DayEnded;
        public event UnityAction DayStarted;

        [Inject]
        private void Init(GameInput inputController)
        {
            _inputController = inputController;
        }

        public void EndDay()
        {
            _inputController.Disable();

            DayEnded?.Invoke();
            _changeTransition.Transit(StartDay);
        }

        public void StartDay()
        {
            _inputController.Enable();

            CurrentDay++;
            DayStarted?.Invoke();
        }
    }
}