using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.Fabrics.Base;

namespace DoubleDTeam.Fabrics
{
    public class PrefabFabricInitializer : InitializeObject
    {
        public override void Initialize()
        {
            Services.ProjectContext.RegisterModule(new PrefabFabric() as IPrefabFabric);
        }

        public override void Deinitialize()
        {
            Services.ProjectContext.RemoveModule<IPrefabFabric>();
        }
    }
}