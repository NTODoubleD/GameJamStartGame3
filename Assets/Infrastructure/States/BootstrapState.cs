using DoubleDCore.Automat.Base;
using Zenject;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;

        [Inject]
        public BootstrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _stateMachine.Enter<LoadResourceState>();
        }

        public void Exit()
        {
        }
    }
}