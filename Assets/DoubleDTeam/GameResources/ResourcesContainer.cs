using System;
using System.Collections.Generic;
using System.IO;
using DoubleDTeam.GameResources.Base;

namespace DoubleDTeam.GameResources
{
    public class ResourcesContainer : IResourcesContainer
    {
        private readonly Dictionary<Type, IResource> _dictionary = new();

        public void AddResource<T>(T resource) where T : IResource
        {
            var resourceType = typeof(T);

            if (_dictionary.ContainsKey(resourceType))
                throw new InvalidDataException($"Attempt to register a registered resource {resourceType.Name}");

            _dictionary.Add(resource.GetType(), resource);

            resource.Load();
        }

        public T GetResource<T>() where T : IResource
        {
            var resourceType = typeof(T);

            if (_dictionary.ContainsKey(resourceType) == false)
                throw new InvalidDataException($"Attempt to contact an unregistered resource {resourceType.Name}");

            return (T)_dictionary[resourceType];
        }

        public bool ContainsResource<T>() where T : IResource
        {
            return _dictionary.ContainsKey(typeof(T));
        }

        public bool ContainsResource(IResource resource)
        {
            return _dictionary.ContainsKey(resource.GetType()) && _dictionary[resource.GetType()] == resource;
        }

        public void RemoveResource<T>() where T : IResource
        {
            if (ContainsResource<T>() == false)
                return;

            var value = _dictionary[typeof(T)];
            _dictionary.Remove(typeof(T));
            value.Dispose();
        }

        public void RemoveResource(IResource resource)
        {
            if (ContainsResource(resource) == false)
                return;

            _dictionary.Remove(resource.GetType());
            resource.Dispose();
        }

        public void Clear()
        {
            foreach (var resource in _dictionary.Values)
                resource.Dispose();

            _dictionary.Clear();
        }
    }
}