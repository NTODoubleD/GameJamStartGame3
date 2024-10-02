using DoubleDCore.Automat.Base;

namespace Infrastructure.GameplayStates
{
    public class MapState : IState
    {
        private readonly GameInput _gameInput;

        public MapState(GameInput gameInput)
        {
            _gameInput = gameInput;
        }

        public void Enter()
        {
            _gameInput.Map.Enable();
        }

        public void Exit()
        {
            _gameInput.Map.Disable();
        }
    }
}