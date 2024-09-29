using DoubleDCore.Automat.Base;

namespace Infrastructure.GameplayStates
{
    public class UIState : IState
    {
        private readonly GameInput _gameInput;

        public UIState(GameInput gameInput)
        {
            _gameInput = gameInput;
        }

        public void Enter()
        {
            _gameInput.UI.Enable();
        }

        public void Exit()
        {
            _gameInput.UI.Disable();
        }
    }
}