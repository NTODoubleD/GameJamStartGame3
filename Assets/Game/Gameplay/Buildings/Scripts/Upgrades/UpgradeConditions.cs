using DoubleDTeam.Containers;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public interface IUpgradeCondition
    {
        bool IsCompleted();
        void Accept(IUpgradeConditionVisitor visitor);
    }

    [Serializable]
    public class TownHallUpgradeCondition : IUpgradeCondition
    {
        [SerializeField] private int _necessaryLevel;

        public int NecessaryLevel => _necessaryLevel;
        public int CurrentLevel => TownHallLocator.Instance.TownHall.CurrentLevel;

        bool IUpgradeCondition.IsCompleted()
        {
            return TownHallLocator.Instance.TownHall.CurrentLevel >= _necessaryLevel;
        }

        void IUpgradeCondition.Accept(IUpgradeConditionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [Serializable]
    public class ResourcesUpgradeCondition : IUpgradeCondition
    {
        [SerializeField] private ItemCount[] _neccessaryItems;

        private Dictionary<ItemInfo, int> _itemsDictionary;

        public IReadOnlyDictionary<ItemInfo, int> NeccessaryItems
        {
            get
            {
                if (_itemsDictionary == null)
                {
                    _itemsDictionary = new();

                    foreach (var itemCount in _neccessaryItems)
                        _itemsDictionary.Add(itemCount.ItemInfo, itemCount.Count);
                }

                return _itemsDictionary;
            }
        }

        void IUpgradeCondition.Accept(IUpgradeConditionVisitor visitor)
        {
            visitor.Visit(this);
        }

        bool IUpgradeCondition.IsCompleted()
        {
            ItemStorage storage = Services.ProjectContext.GetModule<ItemStorage>();

            foreach (var neccessaryItem in NeccessaryItems)
                if (storage.CanRemoveItems(neccessaryItem.Key, neccessaryItem.Value) == false)
                    return false;

            return true;
        }

        [Serializable]
        private class ItemCount
        {
            [SerializeField] private ItemInfo _itemInfo;
            [SerializeField] private int _count;

            public ItemInfo ItemInfo => _itemInfo;
            public int Count => _count;
        }
    }
}