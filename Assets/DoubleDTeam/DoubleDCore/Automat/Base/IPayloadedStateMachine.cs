namespace DoubleDCore.Automat.Base
{
    public interface IPayloadedStateMachine : IBaseStateMachine
    {
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}