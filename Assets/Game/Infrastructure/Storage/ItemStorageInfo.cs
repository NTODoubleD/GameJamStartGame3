using System;
using System.Collections.Generic;
using System.Linq;
using Game.Infrastructure.Items;
using UnityEngine;

namespace Game.Infrastructure.Storage
{
    [Serializable]
    public class ItemStorageInfo
    {
        [SerializeField] private StartItem[] _startItems;

        private Dictionary<ItemInfo, int> _itemsDictionary;

        public IReadOnlyDictionary<ItemInfo, int> StartItems
        {
            get
            {
                if (_itemsDictionary == null)
                    InitializeDictionary();

                return _itemsDictionary;
            }
        }

        private void InitializeDictionary()
        {
            _itemsDictionary = new();

            foreach (var item in _startItems.ToHashSet())
                _itemsDictionary.Add(item.ItemInfo, item.Count);
        }

        [Serializable]
        private class StartItem
        {
            [SerializeField] private ItemInfo _itemInfo;
            [SerializeField] private int _count;

            public ItemInfo ItemInfo => _itemInfo;
            public int Count => _count;
        }
    }
}