using System.Collections;
using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.UI.Pages;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.DayCycle
{
    public class DayChangeTransition : MonoBehaviour
    {
        [SerializeField] private Animator _dimmedScreenAnimator;
        [SerializeField] private string _animationTrigger = "Appear";
        [SerializeField] private float _animationDuration;

        private IUIManager _uiManager;

        private void Awake()
        {
            _uiManager = Services.ProjectContext.GetModule<IUIManager>();
        }

        public void Transit(UnityAction endCallback)
        {
            _uiManager.OpenPage<SimplePage>();

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