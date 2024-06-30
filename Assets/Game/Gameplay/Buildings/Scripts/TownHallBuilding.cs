using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.Infrastructure.Items;
using Game.UI.Pages;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class TownHallBuilding : UpgradableBuilding
    {
        [SerializeField] private List<ItemInfo> _itemInfos;

        private IUIManager _uiManager;

        private void Awake()
        {
            _uiManager = Services.ProjectContext.GetModule<IUIManager>();
        }

        public void OpenUpgradePage()
        {
            _uiManager.OpenPage<UpgradePage, UpgradeMenuArgument>(GetUpgradeMenuArgument());
        }

        private UpgradeMenuArgument GetUpgradeMenuArgument()
        {
            return new UpgradeMenuArgument()
            {
                Label = "Улучшить жилище",
                ItemsDictionary = new Dictionary<ItemInfo, int>
                {
                    { _itemInfos[0], 10 },
                    { _itemInfos[1], 1 },
                    { _itemInfos[2], 30 }
                },
                UpgradableBuilding = this
            };
        }
    }
}