using DoubleDCore.Automat.Base;
using DoubleDCore.UI.Base;
using Game.UI.Pages;

namespace Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly GameInput _gameInput;
        private readonly IUIManager _uiManager;

        public MainMenuState(GameInput gameInput, IUIManager uiManager)
        {
            _gameInput = gameInput;
            _uiManager = uiManager;
        }

        public void Enter()
        {
            _gameInput.UI.Enable();

            _uiManager.OpenPage<MainMenuPage>();
        }

        public void Exit()
        {
        }
    }
}