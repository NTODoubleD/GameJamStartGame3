using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.GameResources.Base;

namespace DoubleDTeam.GameResources
{
    public class ResourcesContainerInitializer : InitializeObject
    {
        public override void Initialize()
        {
            Services.ProjectContext.RegisterModule(new ResourcesContainer() as IResourcesContainer);
        }

        public override void Deinitialize()
        {
            Services.ProjectContext.RemoveModule<IResourcesContainer>();
        }
    }
}