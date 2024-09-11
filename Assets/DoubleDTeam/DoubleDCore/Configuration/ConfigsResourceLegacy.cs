using System;
using System.Collections.Generic;
using DoubleDCore.Configuration.Base;
using DoubleDCore.GameResources.Base;
using UnityEngine;

namespace DoubleDCore.Configuration
{
    public class ConfigsResourceLegacy : IConfigsResource, IResource
    {
        private const string ConfigsPaths = "Config";

        private IResourcesContainer _resourcesContainer;

        private readonly Dictionary<Type, ScriptableObject> _configs = new();

        public TConfigType GetConfig<TConfigType>() where TConfigType : ScriptableObject
        {
            var type = typeof(TConfigType);

            if (_configs.ContainsKey(type) == false)
                return null;

            return _configs[type] as TConfigType;
        }

        private void BindConfig(ScriptableObject scriptableObject)
        {
            var type = scriptableObject.GetType();

            _configs.TryAdd(type, scriptableObject);
        }

        public void Load()
        {
            var configs = Resources.LoadAll<ScriptableObject>(ConfigsPaths);

            foreach (var config in configs)
                BindConfig(config);
        }

        public void Release()
        {
            _configs.Clear();
        }
    }
}