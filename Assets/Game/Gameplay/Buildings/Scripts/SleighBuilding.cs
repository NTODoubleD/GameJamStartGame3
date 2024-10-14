using Game.Infrastructure.Items;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class SleighBuilding : UpgradableBuilding
    {
        [SerializeField] private SleighLevelsConfig _levelsConfig;
        [SerializeField] private SoundReactions _soundReactions;

        public int DeerCapacity => _levelsConfig.GetStatsAt(CurrentLevel).DeerCapacity;

        public int[] GetItemLevelCounts(ItemInfo item)
        {
            return _levelsConfig.GetStatsAt(CurrentLevel).ItemCountLevels[item];
        }

        protected override void OnUpgraded()
        {
            //_soundReactions.Play();
        }

        public override ILevelsConfig GetLevelsConfig()
        {
            return _levelsConfig;
        }
    }
}