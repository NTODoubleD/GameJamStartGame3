using DoubleDTeam.Containers;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using Game.Gameplay;
using TMPro;
using UnityEngine;

namespace Game.UI.Pages
{
    public class DeerInfoPage : MonoPage, IPayloadPage<DeerInfo>
    {
        [SerializeField] private RectTransform _widgetRoot;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private TextMeshProUGUI _text;

        private DeerInfo _currentInfo;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;

            var uiManager = Services.ProjectContext.GetModule<IUIManager>();
            uiManager.ClosePage<DeerInfoPage>();
        }

        public void Open(DeerInfo context)
        {
            SetCanvasState(true);

            _currentInfo = context;

            _text.text = $"Name - {context.Name}\n" +
                         $"Gender - {context.Gender}\n" +
                         $"Age - {context.Age}\n" +
                         $"Hunger - {context.HungerDegree * 100}%\n" +
                         $"Status - {context.Status}";
        }

        public override void Close()
        {
            SetCanvasState(false);

            _currentInfo = null;
        }

        private void Update()
        {
            if (_currentInfo == null)
                return;

            _widgetRoot.anchoredPosition = _mainCamera.WorldToScreenPoint(_currentInfo.WorldPosition);
            _widgetRoot.anchoredPosition += _offset;
        }
    }
}