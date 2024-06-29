using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.DayCycle
{
    public class DayChangeTransition : MonoBehaviour
    {
        [SerializeField] private Animator _dimmedScreenAnimator;
        [SerializeField] private string _animationTrigger = "Appear";
        [SerializeField] private float _animationDuration;

        public void Transit(UnityAction endCallback)
        {
            _dimmedScreenAnimator.SetTrigger(_animationTrigger);
            StartCoroutine(CallDelayed(endCallback));
        }

        private IEnumerator CallDelayed(UnityAction endCallback)
        {
            yield return new WaitForSeconds(_animationDuration);
            endCallback?.Invoke();
        }
    }
}