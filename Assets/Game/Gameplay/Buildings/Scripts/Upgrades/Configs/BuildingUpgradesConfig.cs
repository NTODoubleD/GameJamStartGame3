using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "Building Upgrades Config", menuName = "Upgrades/Building Config")]
    public class BuildingUpgradesConfig : ScriptableObject
    {
        [SerializeField] private LevelData[] _levels;

        public int MaximalLevel => 1 + _levels.Length;

        public IEnumerable<IUpgradeCondition> GetUpgradeConditions(int currentLevel)
        {
            return _levels[currentLevel - 1].Conditions;
        }

        public IEnumerable<IUpgradeCondition> GetAllUpgradeConditions()
        {
            List<IUpgradeCondition> result = new List<IUpgradeCondition>();
            
            foreach (var level in _levels)
                result.AddRange(level.Conditions);

            return result;
        }

        public int GetUpgradeDuration(int currentLevel)
        {
            return _levels[currentLevel - 1].DayDuration;
        }

        [Serializable]
        private class LevelData
        {
            [SerializeField] private int _dayDuration;
            [SerializeReference] private List<IUpgradeCondition> _upgradeConditions = new();

            public IEnumerable<IUpgradeCondition> Conditions => _upgradeConditions;
            public int DayDuration => _dayDuration;
        }
    }
}