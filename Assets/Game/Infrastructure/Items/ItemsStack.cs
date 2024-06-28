using System;
using UnityEngine;

namespace Game.Infrastructure.Items
{
    [Serializable]
    public class ItemsStack
    {
        [SerializeField] private ItemInfo _itemInfo;
        [SerializeField] private int _amount;

        public ItemInfo ItemInfo => _itemInfo;
        public int Amount => _amount;

        public ItemsStack(ItemInfo itemInfo, int amount)
        {
            _itemInfo = itemInfo;
            _amount = amount;
        }
    }
}