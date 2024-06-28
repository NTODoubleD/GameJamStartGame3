using System.Collections.Generic;
using UnityEngine;

namespace DoubleDTeam.Containers.Base
{
    public abstract class ModuleRegister : MonoBehaviour
    {
        [SerializeField] private List<InitializeObject> _initializeObjects;

        private void Awake()
        {
            _initializeObjects.RemoveAll(i => i == null);

            foreach (var initializeObject in _initializeObjects)
                initializeObject.Initialize();

            MonoModule[] modules =
                FindObjectsByType<MonoModule>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            Initialize(modules);
        }

        private void OnDestroy()
        {
            foreach (var initializeObject in _initializeObjects)
                initializeObject.Deinitialize();

            MonoModule[] modules =
                FindObjectsByType<MonoModule>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            Deinitialize(modules);
        }

        protected abstract void Initialize(IEnumerable<MonoModule> modules);
        protected abstract void Deinitialize(IEnumerable<MonoModule> modules);
    }
}