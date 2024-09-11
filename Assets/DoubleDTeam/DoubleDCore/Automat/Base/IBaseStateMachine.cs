namespace DoubleDCore.Automat.Base
{
    public interface IBaseStateMachine
    {
        public IExitableState CurrentState { get; }

        public void BindState(IExitableState state);

        public TState GetState<TState>() where TState : class, IExitableState;
    }
}