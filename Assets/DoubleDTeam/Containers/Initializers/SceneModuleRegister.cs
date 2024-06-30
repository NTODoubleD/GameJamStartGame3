using System;
using System.Collections.Generic;
using DoubleDTeam.Containers.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoubleDTeam.Containers.Initializers
{
    public sealed class SceneModuleRegister : ModuleRegister
    {
        protected override void Initialize(IEnumerable<MonoModule> modules)
        {
            var registers =
                FindObjectsByType<SceneModuleRegister>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            if (registers.Length > 1)
            {
                throw new Exception($"There is more than one {nameof(SceneModuleRegister)}" +
                                    $" on {SceneManager.GetActiveScene().name} scene");
            }

            foreach (var module in modules)
            {
                if (Services.SceneContext.ContainsModule(module))
                    continue;

                Services.SceneContext.RegisterModule((IModule)module);
            }
        }

        protected override void Deinitialize(IEnumerable<MonoModule> modules)
        {
            foreach (var module in modules)
            {
                if (Services.SceneContext.ContainsModule(module) == false)
                    continue;

                Services.SceneContext.RemoveModule(module);
            }

            Services.SceneContext.Clear();
        }
    }
}