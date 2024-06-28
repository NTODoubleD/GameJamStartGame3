using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.SaveSystem.Base;
using DoubleDTeam.SaveSystem.Savers;

namespace DoubleDTeam.SaveSystem
{
    public class SaveControllerInitializer : InitializeObject
    {
        public override void Initialize()
        {
            Services.ProjectContext.RegisterModule(new FileSaver() as ISaveController);
        }

        public override void Deinitialize()
        {
            Services.ProjectContext.RemoveModule<ISaveController>();
        }
    }
}