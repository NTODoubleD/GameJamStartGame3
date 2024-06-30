using System.Linq;
using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.UI.Pages;

namespace Game.Gameplay.Buildings
{
    public class TownHallBuilding : UpgradableBuilding
    {
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
                DayDuration = _upgradesConfig.GetUpgradeDuration(CurrentLevel),
                Conditions = _upgradesConfig.GetUpgradeConditions(CurrentLevel).ToList(),
                UpgradableBuilding = this
            };
        }
    }
}