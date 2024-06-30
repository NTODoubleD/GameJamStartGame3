using System;
using System.Collections.Generic;
using DoubleDTeam.Containers.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoubleDTeam.Containers.Initializers
{
    public sealed class ProjectModuleRegister : ModuleRegister
    {
        protected override void Initialize(IEnumerable<MonoModule> modules)
        {
            DontDestroyOnLoad(this);

            var registers =
                FindObjectsByType<ProjectModuleRegister>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            if (registers.Length > 1)
            {
                throw new Exception($"There is more than one {nameof(ProjectModuleRegister)}" +
                                    $" on {SceneManager.GetActiveScene().name} scene");
            }

            foreach (var module in modules)
            {
                if (Services.ProjectContext.ContainsModule(module))
                    continue;

                Services.ProjectContext.RegisterModule((IModule)module);
            }
        }

        protected override void Deinitialize(IEnumerable<MonoModule> modules)
        {
            foreach (var module in modules)
            {
                if (Services.ProjectContext.ContainsModule(module) == false)
                    continue;

                Services.ProjectContext.RemoveModule(module);
            }

            Services.ProjectContext.Clear();
        }
    }
}