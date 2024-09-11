using System;
using UnityEngine;
using UnityEngine.UI;

namespace DoubleDCore.UI.Base
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonListener : MonoBehaviour
    {
        public Button Button { get; private set; }

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

        protected virtual void Awake()
        {
            Button = GetComponent<Button>();
        }

        protected virtual void OnEnable()
        {
            Button.onClick.AddListener(OnButtonClicked);
        }

        protected virtual void OnDisable()
        {
            Button.onClick.RemoveListener(OnButtonClicked);
        }

        public void SetActiveButton(bool isActive)
        {
            Button.interactable = isActive;

            IsActive = isActive;
        }

        protected abstract void OnButtonClicked();
    }
}