using DoubleDCore.UI.Base;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.UI.Pages
{
    public class AdditionalInfoOpener : ITickable
    {
        private readonly GameInput _gameInput;
        private readonly IUIManager _uiManager;
        private readonly AdditionalInfoOpenConfig _config;

        private float _timeToClose;

        public AdditionalInfoOpener(GameInput gameInput, IUIManager uiManager, 
            AdditionalInfoOpenConfig config)
        {
            _gameInput = gameInput;
            _uiManager = uiManager;
            _config = config;

            _gameInput.Player.AdditionalInfoOpen.started += OnAdditionalInfoOpenRequested;
            _gameInput.UI.AdditionalInfoOpen.started += OnAdditionalInfoOpenRequested;
        }
        
        public void Tick()
        {
            if (_gameInput.Player.AdditionalInfoOpen.IsPressed()
                || _gameInput.UI.AdditionalInfoOpen.IsPressed()
                || _timeToClose <= 0)
                return;

            _timeToClose -= Time.deltaTime;
            
            if (_timeToClose <= 0)
                CloseAdditionalInfo();
        }

        private void OnAdditionalInfoOpenRequested(InputAction.CallbackContext _)
        {
            _uiManager.OpenPage<ResourcePage>();
            _timeToClose = _config.OpenTime;
        }
        
        private void CloseAdditionalInfo()
        {
            _uiManager.ClosePage<ResourcePage>();
        }
        
        ~AdditionalInfoOpener()
        {
            _gameInput.Player.AdditionalInfoOpen.performed -= OnAdditionalInfoOpenRequested;
            _gameInput.UI.AdditionalInfoOpen.performed -= OnAdditionalInfoOpenRequested;
        }
    }
}