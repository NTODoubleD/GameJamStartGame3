using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.Gameplay.AI;
using Game.UI.Pages;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class PastureBuilding : UpgradableBuilding
    {
        [SerializeField] private PastureLevelsConfig _levelsConfig;
        [SerializeField] private WalkablePlane _walkablePlane;
        [SerializeField] private int _testCount;

        private IUIManager _uiManager;

        public int DeerCount => _testCount;

        private void Awake()
        {
            _uiManager = Services.ProjectContext.GetModule<IUIManager>();
        }

        public int GetDeerCapacity(DeerAge deerAge)
        {
            return _levelsConfig.GetStatsAt(CurrentLevel).Capacities[deerAge];
        }

        protected override void OnUpgraded()
        {
            _walkablePlane.transform.localPosition = _levelsConfig.GetStatsAt(CurrentLevel).CenterPoint;
            _walkablePlane.SetWidth(_levelsConfig.GetStatsAt(CurrentLevel).Width);
        }

        public void OpenUpgradePage()
        {
            _uiManager.OpenPage<UpgradePage, UpgradeMenuArgument>(GetUpgradeMenuArgument());
        }

        private UpgradeMenuArgument GetUpgradeMenuArgument()
        {
            return new UpgradeMenuArgument()
            {
                Label = "Улучшить пастбище",
                DayDuration = _upgradesConfig.GetUpgradeDuration(CurrentLevel),
                Conditions = _upgradesConfig.GetUpgradeConditions(CurrentLevel).ToList(),
                UpgradableBuilding = this
            };
        }
    }
}