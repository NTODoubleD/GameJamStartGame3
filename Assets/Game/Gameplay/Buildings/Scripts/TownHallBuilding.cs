using System.Collections.Generic;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using Game.UI.Pages;
using Zenject;

namespace Game.Gameplay.Buildings
{
    public class TownHallBuilding : UpgradableBuilding
    {
        private ItemStorage _itemStorage;

        [Inject]
        private void Init(ItemStorage itemStorage)
        {
            _itemStorage = itemStorage;
        }

        public void OpenResourcePage()
        {
            UIManager.OpenPage<ResourceWatcherPage, ResourcePageArgument>(new ResourcePageArgument()
            {
                Label = "Ресурсы",
                Resource = new Dictionary<ItemInfo, int>(_itemStorage.Resources)
            });
        }
    }
}