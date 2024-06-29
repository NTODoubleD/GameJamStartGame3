using DoubleDTeam.StateMachine;
using DoubleDTeam.StateMachine.Base;
using UnityEngine.SceneManagement;

namespace Game.States
{
    internal class LoadLevelState : IPayloadedState<int>
    {
        private readonly StateMachine _stateMachine;

        public LoadLevelState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(int payload)
        {
            SceneManager.LoadScene(payload, LoadSceneMode.Single);

            _stateMachine.Enter<MainGameBootstrap>();
        }

        public void Exit()
        {
        }
    }
}