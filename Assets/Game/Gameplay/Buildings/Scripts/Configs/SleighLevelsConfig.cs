using Game.Infrastructure.Items;
using System;
using System.Collections.Generic;
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
        [SerializeField] ItemLevels[] _itemLevels;

        private Dictionary<ItemInfo, int[]> _itemLevelsDictionary;

        public int DeerCapacity => _deerCapacity;
        public IReadOnlyDictionary<ItemInfo, int[]> ItemCountLevels
        {
            get
            {
                if (_itemLevelsDictionary == null)
                {
                    _itemLevelsDictionary = new();

                    foreach (var itemLevel in _itemLevels)
                        _itemLevelsDictionary.Add(itemLevel.Item, itemLevel.Levels);
                }

                return _itemLevelsDictionary;
            }
        }

        [Serializable]
        private class ItemLevels
        {
            [SerializeField] private ItemInfo _item;
            [SerializeField] private int[] _levels;

            public int[] Levels => _levels;
            public ItemInfo Item => _item;
        }
    }
}