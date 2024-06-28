using System.Linq;
using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.GameResources.Base;
using UnityEngine;

namespace DoubleDTeam.GameResources
{
    public class ConfigInitializer : InitializeObject
    {
        [SerializeField] private string _configsPaths = "Config";

        public override void Initialize()
        {
            var projectContext = Services.ProjectContext;
            var resourceContainer = projectContext.GetModule<IResourcesContainer>();

            var configs = Resources.LoadAll<ScriptableObject>(_configsPaths).OfType<IResource>();

            foreach (var config in configs)
                resourceContainer.AddResource(config);
        }

        public override void Deinitialize()
        {
        }
    }
}