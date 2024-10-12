using DoubleDCore.UI.Base;
using Game.UI.Data;
using Game.UI.Pages;
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

        public void StartTraining(TrainingInfo context)
        {
            _gameInput.Player.Disable();
            _gameInput.Map.Disable();

            _gameInput.UI.Enable();

            _uiManager.OpenPage<TrainingPage, TrainingPageArgument>(new TrainingPageArgument
            {
                TrainingInfo = context,
                OnClose = OnStopTraining
            });
        }

        private void OnStopTraining()
        {
            _gameInput.Player.Enable();
            _gameInput.Map.Enable();

            _gameInput.UI.Disable();
        }
    }
}