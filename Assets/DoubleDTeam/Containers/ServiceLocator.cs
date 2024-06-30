using System;
using System.Collections.Generic;
using DoubleDTeam.Containers.Base;

namespace DoubleDTeam.Containers
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, IModule> _modules = new();

        public bool ContainsModule<T>() where T : class, IModule
        {
            var type = typeof(T);

            return _modules.ContainsKey(type);
        }

        public bool ContainsModule(IModule module)
        {
            foreach (var (_, registerModule) in _modules)
            {
                if (module == registerModule)
                    return true;
            }

            return false;
        }

        public void RegisterModule<T>(T module) where T : class, IModule
        {
            var type = typeof(T);

            if (ContainsModule<T>())
                throw new InvalidOperationException($"Attempt to register an existing module {type.Name}");

            _modules.Add(type, module);
        }

        public void RegisterModule(IModule module)
        {
            var type = module.GetType();

            if (ContainsModule(module))
                throw new InvalidOperationException($"Attempt to register an existing module {type.Name}");

            _modules.Add(type, module);
        }

        public T GetModule<T>() where T : class, IModule
        {
            var type = typeof(T);

            if (ContainsModule<T>() == false)
                throw new InvalidOperationException($"Attempt to access an unregistered module {type.Name}");

            return _modules[type] as T;
        }

        public T RemoveModule<T>() where T : class, IModule
        {
            if (ContainsModule<T>() == false)
                return null;

            var moduleType = typeof(T);

            var result = _modules[moduleType];

            _modules.Remove(moduleType);

            return result as T;
        }

        public void RemoveModule(IModule module)
        {
            var killMarks = new List<Type>();

            foreach (var (type, registerModule) in _modules)
            {
                if (registerModule == module)
                    killMarks.Add(type);
            }

            foreach (var killMark in killMarks)
                _modules.Remove(killMark);
        }

        public void Clear()
        {
            _modules.Clear();
        }
    }
}