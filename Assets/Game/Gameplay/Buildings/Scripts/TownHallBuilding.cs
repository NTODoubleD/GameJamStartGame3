using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using Game.UI.Pages;

namespace Game.Gameplay.Buildings
{
    public class TownHallBuilding : UpgradableBuilding
    {
        public void OpenResourcePage()
        {
            var storage = Services.ProjectContext.GetModule<ItemStorage>();
            var uiManager = Services.ProjectContext.GetModule<IUIManager>();

            uiManager.OpenPage<ResourceWatcherPage, ResourcePageArgument>(new ResourcePageArgument()
            {
                Label = "Ресурсы",
                Resource = new Dictionary<ItemInfo, int>(storage.Resources)
            });
        }
    }
}