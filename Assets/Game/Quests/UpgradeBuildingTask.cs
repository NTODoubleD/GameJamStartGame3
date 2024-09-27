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