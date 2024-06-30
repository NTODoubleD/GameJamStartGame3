using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.DayCycle
{
    public class DayChangeTransition : MonoBehaviour
    {
        [SerializeField] private Animator _dimmedScreenAnimator;
        [SerializeField] private FactDisplayer _factDisplayer;
        [SerializeField] private NewDayDisplayer _newDayDisplayer;
        [SerializeField] private string _appearTrigger = "Appear";
        [SerializeField] private string _disappearTrigger = "Disappear";
        [SerializeField] private float _showDuration;

        public void Transit(UnityAction endCallback)
        {
            _factDisplayer.DisplayRandomFact();
            _newDayDisplayer.DisplayNewDay();
            _dimmedScreenAnimator.SetTrigger(_appearTrigger);
            StartCoroutine(CallDelayed(endCallback));
        }

        private IEnumerator CallDelayed(UnityAction endCallback)
        {
            yield return new WaitForSeconds(_showDuration);
            _dimmedScreenAnimator.SetTrigger(_disappearTrigger);
            endCallback?.Invoke();
        }
    }
}