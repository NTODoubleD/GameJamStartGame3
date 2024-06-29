using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class SleighBuilding : UpgradableBuilding
    {
        [SerializeField] private SleighLevelsConfig _levelsConfig;

        public int DeerCapacity => _levelsConfig.GetStatsAt(CurrentLevel).DeerCapacity;
        public int ItemCapacity => _levelsConfig.GetStatsAt(CurrentLevel).ItemCapacity;
    }
}