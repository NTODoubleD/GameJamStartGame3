using DoubleDTeam.Containers;
using DoubleDTeam.StateMachine;
using DoubleDTeam.StateMachine.Base;

namespace Game.States
{
    public class BootstrapState : IPayloadedState<int>
    {
        private StateMachine _stateMachine;

        public void Enter(int nextScene)
        {
            _stateMachine ??= Services.ProjectContext.GetModule<StateMachine>();

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