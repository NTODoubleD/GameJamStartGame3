﻿using System;
using System.Collections.Generic;
using DoubleDTeam.Containers.Base;
using Game.Infrastructure.Items;
using UnityEngine;

namespace Game.Infrastructure.Storage
{
    public class ItemStorage : IModule
    {
        private readonly Dictionary<ItemInfo, int> _items = new();

        public ItemStorage()
        {

        }

        public ItemStorage(ItemStorageInfo info)
        {
            foreach (var keyPair in info.StartItems)
                AddItems(keyPair.Key, keyPair.Value);
        }

        public event Action<ItemInfo, int> ItemAdded;
        public event Action<ItemInfo, int> ItemRemoved;

        public void AddItems(ItemInfo item, int count)
        {
            if (_items.ContainsKey(item))
                _items[item] += count;
            else
                _items.Add(item, count);

            ItemAdded?.Invoke(item, count);
        }

        public void RemoveItems(ItemInfo item, int count)
        {
            _items[item] = Mathf.Max(0, _items[item] - count);
            ItemRemoved?.Invoke(item, count);
        }

        public bool CanRemoveItems(ItemInfo item, int count)
        {
            if (_items.ContainsKey(item) == false)
                return false;

            return _items[item] >= count;
        }

        public int GetCount(ItemInfo item)
        {
            if (_items.ContainsKey(item) == false)
                return 0;
            
            return _items[item];
        }
    }
}