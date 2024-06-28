using DoubleDTeam.StateMachine;
using DoubleDTeam.StateMachine.Base;
using Game.Static;

namespace Game.States
{
    public class BootstrapState : IState
    {
        private GameStateMachine _stateMachine;

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(SceneNames.Main);
        }
    }
}