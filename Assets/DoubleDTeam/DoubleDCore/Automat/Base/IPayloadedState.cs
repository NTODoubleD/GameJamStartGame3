namespace DoubleDCore.Automat.Base
{
    public interface IPayloadedState<in TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}