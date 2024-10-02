using DoubleDCore.Automat.Base;

namespace Infrastructure.GameplayStates
{
    public class PlayerMovingState : IState
    {
        private readonly GameInput _gameInput;

        public PlayerMovingState(GameInput gameInput)
        {
            _gameInput = gameInput;
        }

        public void Enter()
        {
            _gameInput.Player.Enable();
        }

        public void Exit()
        {
            _gameInput.Player.Disable();
        }
    }
}