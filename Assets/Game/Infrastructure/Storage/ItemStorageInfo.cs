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
        [SerializeField] private int _capacity;

        [Space, SerializeField] private ItemInfo[] _whiteList;
        [SerializeField] private ItemInfo[] _blackList;

        public int Capacity => _capacity;
        public IEnumerable<ItemInfo> WhiteList => _whiteList;
        public IEnumerable<ItemInfo> BlackList => _blackList;

        public ItemStorageInfo(int capacity, IEnumerable<ItemInfo> whiteList, IEnumerable<ItemInfo> blackList)
        {
            _capacity = capacity;

            if (whiteList != null)
                _whiteList = whiteList.ToArray();

            if (blackList != null)
                _blackList = blackList.ToArray();
        }
    }
}