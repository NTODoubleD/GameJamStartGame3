namespace DoubleDTeam.StateMachine.Base
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}