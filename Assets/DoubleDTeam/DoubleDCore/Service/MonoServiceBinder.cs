using System.Collections.Generic;
using DoubleDCore.Attributes;
using DoubleDCore.Finder;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace DoubleDCore.Service
{
    public class MonoServiceBinder : MonoInstaller
    {
        [ReadOnlyProperty, SerializeField] private List<MonoService> _services;

        private readonly GameObjectFinder _finder = new();

        public override void InstallBindings()
        {
            foreach (var monoService in _services)
                Container.Bind(monoService.GetType()).FromInstance(monoService).AsCached();
        }

        public void OnDestroy()
        {
            foreach (var monoService in _services)
                Container.Unbind(monoService.GetType());
        }

#if UNITY_EDITOR
        [Button("Add all services")]
        private void AddAllServices()
        {
            var services = _finder.Find<MonoService>();

            _services.Clear();
            _services.AddRange(services);

            EditorUtility.SetDirty(gameObject);
        }
#endif
    }
}