using DoubleDCore.Automat.Base;
using Zenject;

namespace Infrastructure.States
{
    public class LoadSaveState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly BootstrapInfo _bootstrapInfo;

        [Inject]
        private LoadSaveState(GameStateMachine stateMachine, BootstrapInfo bootstrapInfo)
        {
            _stateMachine = stateMachine;
            _bootstrapInfo = bootstrapInfo;
        }

        public void Enter()
        {
            _stateMachine.Enter<LoadSceneState, LoadScenePayload>(
                new LoadScenePayload(_bootstrapInfo.NextSceneName,
                    AfterLoad: () => _stateMachine.Enter<MainMenuState>()));
        }

        public void Exit()
        {
        }
    }
}