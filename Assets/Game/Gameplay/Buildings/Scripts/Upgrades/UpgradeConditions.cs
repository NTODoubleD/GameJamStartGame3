using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using System;
using System.Collections.Generic;
using Game.Gameplay.Items;
using UnityEngine;
using Zenject;

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

        private Dictionary<GameItemInfo, int> _itemsDictionary;

        private ItemStorage _itemStorage;

        [Inject]
        private void Init(ItemStorage itemStorage)
        {
            _itemStorage = itemStorage;
        }

        public IReadOnlyDictionary<GameItemInfo, int> NeccessaryItems
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
            foreach (var neccessaryItem in NeccessaryItems)
                if (_itemStorage.CanRemoveItems(neccessaryItem.Key, neccessaryItem.Value) == false)
                    return false;

            return true;
        }

        [Serializable]
        private class ItemCount
        {
            [SerializeField] private GameItemInfo _itemInfo;
            [SerializeField] private int _count;

            public GameItemInfo ItemInfo => _itemInfo;
            public int Count => _count;
        }
    }
}