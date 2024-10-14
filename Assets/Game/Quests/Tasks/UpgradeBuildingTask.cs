using Game.Gameplay.Buildings;
using Game.Quests.Base;
using UnityEngine;

namespace Game.Quests
{
    public class UpgradeBuildingTask : YakutSubTask
    {
        [SerializeField] private UpgradableBuilding _building;

        public override void Play()
        {
            if (_building.CurrentLevel > 1)
                Progress = 1;
            else
                _building.Upgraded += BuildingOnUpgraded;
        }

        public override void Close()
        {
            _building.Upgraded -= BuildingOnUpgraded;
        }

        private void BuildingOnUpgraded()
        {
            Progress = 1;
        }
    }
}