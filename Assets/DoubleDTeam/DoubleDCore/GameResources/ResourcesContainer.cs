using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using DoubleDCore.GameResources.Base;

namespace DoubleDCore.GameResources
{
    public class ResourcesContainer : IResourcesContainer
    {
        private readonly Dictionary<Type, IReleasable> _dictionary = new();
        private readonly Dictionary<Type, List<object>> _callbacks = new();

        public void BindAddingResourceCallback<T>(Action<T> callback) where T : IAsyncResource
        {
            var type = typeof(T);

            if (ContainsResource<T>())
            {
                callback?.Invoke(GetResource<T>());
                return;
            }

            if (_callbacks.TryGetValue(type, out var callbacks))
                callbacks.Add(callback);
            else
                _callbacks.Add(type, new List<object> { callback });
        }

        public void AddResource<T>(T resource) where T : IResource
        {
            var resourceType = typeof(T);

            if (_dictionary.ContainsKey(resourceType))
                throw new InvalidDataException($"Attempt to register a registered resource {resourceType.Name}");

            _dictionary.Add(resource.GetType(), resource);

            resource.Load();
        }

        public async UniTask AddAsyncResource<T>(T resource) where T : IAsyncResource
        {
            var resourceType = typeof(T);

            if (_dictionary.ContainsKey(resourceType))
                throw new InvalidDataException($"Attempt to register a registered resource {resourceType.Name}");

            _dictionary.Add(resource.GetType(), resource);

            await resource.Load();

            InvokeCallbacks<T>();
        }

        public T GetResource<T>() where T : IReleasable
        {
            var resourceType = typeof(T);

            if (_dictionary.ContainsKey(resourceType) == false)
                throw new InvalidDataException($"Attempt to contact an unregistered resource {resourceType.Name}");

            return (T)_dictionary[resourceType];
        }

        public bool ContainsResource<T>() where T : IReleasable
        {
            return _dictionary.ContainsKey(typeof(T));
        }

        public bool ContainsResource(IReleasable resource)
        {
            return _dictionary.ContainsKey(resource.GetType()) && _dictionary[resource.GetType()] == resource;
        }

        public void RemoveResource<T>() where T : IReleasable
        {
            if (ContainsResource<T>() == false)
                return;

            var value = _dictionary[typeof(T)];
            _dictionary.Remove(typeof(T));
            value.Release();
        }

        public void RemoveResource(IReleasable resource)
        {
            if (ContainsResource(resource) == false)
                return;

            _dictionary.Remove(resource.GetType());
            resource.Release();
        }

        public void Clear()
        {
            foreach (var resource in _dictionary.Values)
                resource.Release();

            _dictionary.Clear();
        }

        private void InvokeCallbacks<T>() where T : IAsyncResource
        {
            var type = typeof(T);

            if (_callbacks.ContainsKey(type) == false)
                return;

            var resource = GetResource<T>();

            _callbacks[type].ForEach(c =>
            {
                var action = c as Action<T>;
                action?.Invoke(resource);
            });

            _callbacks.Remove(type);
        }
    }
}