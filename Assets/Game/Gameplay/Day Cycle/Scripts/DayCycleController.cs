using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.InputSystem;
using Game.InputMaps;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.DayCycle
{
    public class DayCycleController : MonoModule
    {
        [SerializeField] private DayChangeTransition _changeTransition;

        private InputController _inputManager;

        public int CurrentDay { get; private set; } = 1;

        public event UnityAction DayEnded;
        public event UnityAction DayStarted;

        public void Awake()
        {
            _inputManager = Services.ProjectContext.GetModule<InputController>();
        }

        public void EndDay()
        {
            _inputManager.DisableActiveMap();

            DayEnded?.Invoke();
            _changeTransition.Transit(StartDay);
        }

        public void StartDay()
        {
            _inputManager.EnableMap<PlayerInputMap>();

            CurrentDay++;
            DayStarted?.Invoke();
        }
    }
}