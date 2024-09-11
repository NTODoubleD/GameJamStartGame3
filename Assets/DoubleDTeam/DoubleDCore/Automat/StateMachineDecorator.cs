using DoubleDCore.Automat.Base;

namespace DoubleDCore.Automat
{
    public class StateMachineDecorator : IFullStateMachine
    {
        protected readonly IFullStateMachine StateMachine;

        public StateMachineDecorator(IFullStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public IExitableState CurrentState => StateMachine.CurrentState;

        public virtual void BindState(IExitableState state)
        {
            StateMachine.BindState(state);
        }

        public TState GetState<TState>() where TState : class, IExitableState
        {
            return StateMachine.GetState<TState>();
        }

        public virtual void Enter<TState>() where TState : class, IState
        {
            StateMachine.Enter<TState>();
        }

        public virtual void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            StateMachine.Enter<TState, TPayload>(payload);
        }
    }
}