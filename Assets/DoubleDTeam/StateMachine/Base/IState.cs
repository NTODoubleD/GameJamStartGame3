namespace DoubleDTeam.StateMachine.Base
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}