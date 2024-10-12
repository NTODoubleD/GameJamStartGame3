using DoubleDCore.UI.Base;
using Game.UI.Data;
using Game.UI.Pages;
using UnityEngine;
using Zenject;

namespace Game.Tips
{
    public class GameTrainingController
    {
        private readonly IUIManager _uiManager;
        private readonly GameInput _gameInput;

        [Inject]
        public GameTrainingController(IUIManager uiManager, GameInput gameInput)
        {
            _uiManager = uiManager;
            _gameInput = gameInput;
        }

        private bool _isPlayerInput;
        private bool _isMapInput;
        private bool _isUIInput;

        public void StartTraining(TrainingInfo context)
        {
            _isPlayerInput = _gameInput.Player.enabled;
            _isMapInput = _gameInput.Map.enabled;
            _isUIInput = _gameInput.UI.enabled;

            _gameInput.Player.Disable();
            _gameInput.Map.Disable();

            _gameInput.UI.Enable();

            Time.timeScale = 0;

            _uiManager.OpenPage<TrainingPage, TrainingPageArgument>(new TrainingPageArgument
            {
                TrainingInfo = context,
                OnClose = OnStopTraining
            });
        }

        private void OnStopTraining()
        {
            if (_isPlayerInput)
                _gameInput.Player.Enable();

            if (_isMapInput)
                _gameInput.Map.Enable();

            if (_isUIInput)
                _gameInput.UI.Enable();
            else
                _gameInput.UI.Disable();
            
            Time.timeScale = 1;
        }
    }
}