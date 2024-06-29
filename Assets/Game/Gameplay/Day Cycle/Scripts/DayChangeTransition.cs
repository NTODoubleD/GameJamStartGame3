using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.DayCycle
{
    public class DayChangeTransition : MonoBehaviour
    {
        public void Transit(UnityAction endCallback)
        {
            endCallback?.Invoke();
        }
    }
}