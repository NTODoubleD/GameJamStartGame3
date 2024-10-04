using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class TooltipPage : MonoPage, IPayloadPage<TooltipArgument>
    {
        [SerializeField] private TMP_Text _tooltipText;
        [SerializeField] private RectTransform _tooltipObject;

        private GameInput _gameInput;
        private Vector2 _mousePosition;
        private Vector2 _localMousePoint;
        private RectTransform _tooltipParent;
        
        [Inject]
        private void Init(GameInput gameInput)
        {
            _gameInput = gameInput;
        }

        public override void Initialize()
        {
            _tooltipParent = _tooltipObject.parent as RectTransform;
        }

        public void Open(TooltipArgument context)
        {
            _tooltipText.text = context.Text;
            SetCanvasState(true);
        }

        private void Update()
        {
            if (!PageIsDisplayed)
                return;
            
            _mousePosition = _gameInput.UI.enabled ? _gameInput.UI.MousePosition.ReadValue<Vector2>() : _gameInput.Player.MousePosition.ReadValue<Vector2>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_tooltipParent, _mousePosition, null, out _localMousePoint);
            _tooltipObject.localPosition = _localMousePoint;
        }

        public override void Close()
        {
            SetCanvasState(false);
        }
    }
    
    public class TooltipArgument
    {
        public string Text;

        public TooltipArgument(string text)
        {
            Text = text;
        }
    }
}