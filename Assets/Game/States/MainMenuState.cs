using DoubleDTeam.InputSystem;
using DoubleDTeam.StateMachine.Base;
using DoubleDTeam.UI.Base;
using Game.InputMaps;
using Game.UI.Pages;

namespace Game.States
{
    public class MainMenuState : IState
    {
        private readonly InputController _inputController;
        private readonly IUIManager _uiManager;

        public MainMenuState(InputController inputController, IUIManager uiManager)
        {
            _inputController = inputController;
            _uiManager = uiManager;
        }

        public void Enter()
        {
            _inputController.EnableMap<UIInputMap>();
            _uiManager.OpenPage<MainMenuPage>();
        }

        public void Exit()
        {
        }
    }
}