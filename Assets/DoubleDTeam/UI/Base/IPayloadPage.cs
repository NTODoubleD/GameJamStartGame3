namespace DoubleDTeam.UI.Base
{
    public interface IPayloadPage<in TPayload> : IPage
    {
        public void Open(TPayload context);
    }
}