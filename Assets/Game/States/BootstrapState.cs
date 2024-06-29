using Cysharp.Threading.Tasks;
using DoubleDTeam.StateMachine;
using DoubleDTeam.StateMachine.Base;
using UnityEngine.SceneManagement;

namespace Game.States
{
    public class BootstrapState : IPayloadedState<int>
    {
        private readonly StateMachine _stateMachine;

        public BootstrapState(StateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }

        public async void Enter(int nextScene)
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);

            await UniTask.NextFrame();

            _stateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
        }
    }
}