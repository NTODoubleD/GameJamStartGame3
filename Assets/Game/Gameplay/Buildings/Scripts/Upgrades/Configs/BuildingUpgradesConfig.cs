using Sirenix.OdinInspector;
using Sirenix.Serialization;
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

        [Serializable]
        private class LevelData
        {
            [SerializeReference] private List<IUpgradeCondition> _upgradeConditions = new();

            public IEnumerable<IUpgradeCondition> Conditions => _upgradeConditions;
        }
    }
}