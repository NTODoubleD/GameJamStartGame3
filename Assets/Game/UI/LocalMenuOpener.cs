using DoubleDCore.Service;
using DoubleDCore.TimeTools;
using DoubleDCore.UI.Base;
using Game.UI.Pages;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.UI
{
    public class LocalMenuOpener : MonoService
    {
        private const float OpenDelay = 0.1f;

        private GameInput _inputController;
        private Timer _timer;
        private IUIManager _uiManager;

        private bool _isOpen;

        [Inject]
        private void Init(GameInput inputController, ITimersFactory timersFactory, IUIManager uiManager)
        {
            _inputController = inputController;
            _timer = timersFactory.Create(TimeBindingType.RealTime);
            _uiManager = uiManager;
        }

        private void OnEnable()
        {
            _inputController.Player.Escape.performed += Open;
            _inputController.UI.CloseMenu.performed += Close;
        }

        private void OnDisable()
        {
            _inputController.Player.Escape.performed -= Open;
            _inputController.UI.CloseMenu.performed -= Close;
        }

        private void Open(InputAction.CallbackContext callbackContext)
        {
            if (_timer.IsWorked)
                return;

            _uiManager.OpenPage<LocalMenuPage>();

            _timer.Start(OpenDelay);

            _isOpen = true;
        }

        private void Close(InputAction.CallbackContext callbackContext)
        {
            if (_timer.IsWorked || _isOpen == false)
                return;

            if (_uiManager.ContainsPage<TutorialPage>())
                _uiManager.ClosePage<TutorialPage>();
            
            _uiManager.ClosePage<LocalMenuPage>();

            _timer.Start(OpenDelay);

            _isOpen = false;
        }
    }
}