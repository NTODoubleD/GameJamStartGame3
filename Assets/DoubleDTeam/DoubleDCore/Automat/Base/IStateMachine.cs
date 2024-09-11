namespace DoubleDCore.Automat.Base
{
    public interface IStateMachine : IBaseStateMachine
    {
        public void Enter<TState>() where TState : class, IState;
    }
}