namespace DoubleDTeam.Containers.Base
{
    public interface IServiceLocator
    {
        public bool ContainsModule<T>() where T : class, IModule;

        public bool ContainsModule(IModule module);

        public void RegisterModule<T>(T module) where T : class, IModule;

        public T GetModule<T>() where T : class, IModule;

        public T RemoveModule<T>() where T : class, IModule;

        public void RemoveModule(IModule module);

        public void Clear();
    }
}