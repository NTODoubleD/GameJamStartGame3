using DoubleDTeam.StateMachine;
using DoubleDTeam.StateMachine.Base;

namespace Game.States
{
    public class BootstrapState : IPayloadedState<int>
    {
        private readonly StateMachine _stateMachine;

        public BootstrapState(StateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }

        public void Enter(int nextScene)
        {
            EnterLoadLevel(nextScene);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel(int sceneIndex)
        {
            _stateMachine.Enter<LoadLevelState, int>(sceneIndex);
        }
    }
}