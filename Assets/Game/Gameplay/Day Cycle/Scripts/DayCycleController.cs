using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using Game.InputMaps;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.DayCycle
{
    public class DayCycleController : MonoBehaviour
    {
        [SerializeField] private DayChangeTransition _changeTransition;

        private InputController _inputManager;

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
            DayStarted?.Invoke();

            _inputManager.EnableMap<PlayerInputMap>();
        }
    }
}