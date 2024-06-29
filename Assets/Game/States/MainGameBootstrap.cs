using DoubleDTeam.InputSystem;
using DoubleDTeam.StateMachine.Base;
using Game.InputMaps;

namespace Game.States
{
    public class MainGameBootstrap : IState
    {
        private readonly InputController _inputController;

        public MainGameBootstrap(InputController inputController)
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