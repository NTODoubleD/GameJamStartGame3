using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.UI.Base;

namespace DoubleDTeam.UI.Initializers
{
    public class UIManagerInitializer : InitializeObject
    {
        public override void Initialize()
        {
            IUIManager uiManager = new UIManager();
            Services.ProjectContext.RegisterModule(uiManager);
        }

        public override void Deinitialize()
        {
            Services.ProjectContext.RemoveModule<IUIManager>();
        }
    }
}