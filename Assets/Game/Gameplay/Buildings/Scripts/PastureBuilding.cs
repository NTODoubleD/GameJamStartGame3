using DoubleDTeam.Containers;
using Game.Gameplay.AI;
using Game.Gameplay.Scripts;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class PastureBuilding : UpgradableBuilding
    {
        [SerializeField] private PastureLevelsConfig _levelsConfig;
        [SerializeField] private WalkablePlane _walkablePlane;

        public int DeerCount => Services.SceneContext.GetModule<Herd>().SuitableDeer.Count;

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