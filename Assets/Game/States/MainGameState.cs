using DoubleDTeam.InputSystem;
using DoubleDTeam.StateMachine.Base;
using Game.InputMaps;

namespace Game.States
{
    public class MainGameState : IState
    {
        private readonly InputController _inputController;

        public MainGameState(InputController inputController)
        {
            _inputController = inputController;
        }

        public void Enter()
        {
            _inputController.EnableMap<PlayerInputMap>();
        }

        public void Exit()
        {
        }
    }
}