using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.Gameplay.AI;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class PastureBuilding : UpgradableBuilding
    {
        [SerializeField] private PastureLevelsConfig _levelsConfig;
        [SerializeField] private WalkablePlane _walkablePlane;
        [SerializeField] private int _testCount;

        public int DeerCount => _testCount;

        public int GetDeerCapacity(DeerAge deerAge)
        {
            return _levelsConfig.GetStatsAt(CurrentLevel).Capacities[deerAge];
        }

        protected override void OnUpgraded()
        {
            _walkablePlane.transform.localPosition = _levelsConfig.GetStatsAt(CurrentLevel).CenterPoint;
            _walkablePlane.SetWidth(_levelsConfig.GetStatsAt(CurrentLevel).Width);
        }
    }
}