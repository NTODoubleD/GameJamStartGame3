using System;
using UnityEngine;

namespace DoubleDCore.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupActivator : MonoBehaviour
    {
        [SerializeField] private bool _startState;

        private CanvasGroup _canvasGroup;

        public event Action<bool> ActiveStateChanged;

        private bool _isActive;

        public bool IsActive
        {
            get => _isActive;
            private set
            {
                _isActive = value;
                ActiveStateChanged?.Invoke(_isActive);
            }
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            SetState(_startState);
        }

        public void SetState(bool isActive)
        {
            _canvasGroup.alpha = isActive ? 1 : 0;
            _canvasGroup.interactable = isActive;
            _canvasGroup.blocksRaycasts = isActive;

            IsActive = isActive;
        }

        public CanvasGroupActivator SetAlpha(bool isActive)
        {
            _canvasGroup.alpha = isActive ? 1 : 0;
            return this;
        }

        public CanvasGroupActivator SetAlpha(float value)
        {
            _canvasGroup.alpha = Mathf.Clamp01(value);
            return this;
        }

        public CanvasGroupActivator SetIntractable(bool isActive)
        {
            _canvasGroup.interactable = isActive;
            return this;
        }

        public CanvasGroupActivator SetBlocksRaycasts(bool isActive)
        {
            _canvasGroup.blocksRaycasts = isActive;
            return this;
        }
    }
}