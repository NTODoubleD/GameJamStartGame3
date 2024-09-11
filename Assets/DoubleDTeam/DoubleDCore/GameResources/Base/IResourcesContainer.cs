using System;
using Cysharp.Threading.Tasks;

namespace DoubleDCore.GameResources.Base
{
    public interface IResourcesContainer
    {
        public void BindAddingResourceCallback<T>(Action<T> callback) where T : IAsyncResource;

        public void AddResource<T>(T resource) where T : IResource;

        public UniTask AddAsyncResource<T>(T resource) where T : IAsyncResource;

        public T GetResource<T>() where T : IReleasable;

        public bool ContainsResource<T>() where T : IReleasable;

        public bool ContainsResource(IReleasable resource);

        public void RemoveResource<T>() where T : IReleasable;

        public void RemoveResource(IReleasable resource);

        public void Clear();
    }
}