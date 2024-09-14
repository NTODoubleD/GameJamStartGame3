using Cysharp.Threading.Tasks;
using DoubleDCore.Automat.Base;

namespace Game.Gameplay.States
{
    public class DeerEatsState : IState
    {
        private readonly IStateMachine _stateMachine;

        public DeerEatsState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async void Enter()
        {
            await UniTask.WaitForSeconds(10f);
            
            _stateMachine.Enter<DeerIdleState>();
        }

        public void Exit()
        {
        }
    }
}