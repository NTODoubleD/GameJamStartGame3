using DoubleDTeam.Containers.Base;

namespace DoubleDTeam.GameResources.Base
{
    public interface IResourcesContainer : IModule
    {
        public void AddResource<T>(T resource) where T : IResource;

        public T GetResource<T>() where T : IResource;

        public bool ContainsResource<T>() where T : IResource;

        public bool ContainsResource(IResource resource);

        public void RemoveResource<T>() where T : IResource;

        public void RemoveResource(IResource resource);

        public void Clear();
    }
}