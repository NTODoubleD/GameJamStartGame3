namespace DoubleDCore.Automat.Base
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}