using System;
using DoubleDCore.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ToggleButton : ButtonListener
    {
        [SerializeField] private Sprite _selectSprite;
        [SerializeField] private Sprite _deselectSprite;
        [SerializeField] private Image _imageComponent;

        private Action _onSelectAction;
        private Action _onDeselectAction;

        private bool _state;

        public bool State => _state;

        public void Initialize(bool state, Action onSelect = null, Action onDeselect = null)
        {
            _onSelectAction = onSelect;
            _onDeselectAction = onDeselect;

            SetState(state);
        }

        public void SetState(bool state)
        {
            _state = state;

            _imageComponent.sprite = _state ? _selectSprite : _deselectSprite;

            CallAction();
        }

        protected override void OnButtonClicked()
        {
            SetState(!_state);
        }

        private void CallAction()
        {
            if (_state)
                _onSelectAction?.Invoke();
            else
                _onDeselectAction?.Invoke();
        }
    }
}