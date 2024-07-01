using Game.Infrastructure.Items;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class SleighBuilding : UpgradableBuilding
    {
        [SerializeField] private SleighLevelsConfig _levelsConfig;

        public int DeerCapacity => _levelsConfig.GetStatsAt(CurrentLevel).DeerCapacity;

        public int[] GetItemLevelCounts(ItemInfo item)
        {
            return _levelsConfig.GetStatsAt(CurrentLevel).ItemCountLevels[item];
        }
    }
}