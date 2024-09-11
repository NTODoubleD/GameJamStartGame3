using Game.Gameplay.AI;
using Game.Gameplay.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Buildings
{
    public class PastureBuilding : UpgradableBuilding
    {
        [SerializeField] private PastureLevelsConfig _levelsConfig;

        private Herd _herd;
        private WalkablePlane _walkablePlane;

        [Inject]
        private void Init(Herd herd, WalkablePlane walkablePlane)
        {
            _herd = herd;
            _walkablePlane = walkablePlane;
        }

        public int DeerCount => _herd.SuitableDeer.Count;

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