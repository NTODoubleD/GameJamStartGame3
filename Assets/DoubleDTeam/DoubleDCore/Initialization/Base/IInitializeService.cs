namespace DoubleDCore.Initialization.Base
{
    public interface IInitializeService
    {
        public void InitializeAll<TType>() where TType : IInitializing;
        public void Initialize(IInitializing initializingObject);
    }
}