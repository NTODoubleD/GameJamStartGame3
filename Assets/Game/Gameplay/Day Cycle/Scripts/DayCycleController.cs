using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.DayCycle
{
    public class DayCycleController : MonoBehaviour
    {
        [SerializeField] private DayChangeTransition _changeTransition;

        public int CurrentDay { get; private set; } = 1;

        public event UnityAction DayEnded;
        public event UnityAction DayStarted;

        public void EndDay()
        {
            DayEnded?.Invoke();
            _changeTransition.Transit(StartDay);
        }

        public void StartDay()
        {
            CurrentDay++;
            DayStarted?.Invoke();
        }
    }
}