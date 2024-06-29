using System;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "Sleight Config", menuName = "Buildings/Sleight Config")]
    public class SleighLevelsConfig : LevelsConfig<SleightLevelStat>
    {
    }

    [Serializable]
    public class SleightLevelStat
    {
        [SerializeField] private int _deerCapacity;
        [SerializeField] private int _itemCapacity;

        public int DeerCapacity => _deerCapacity;
        public int ItemCapacity => _itemCapacity;
    }
}