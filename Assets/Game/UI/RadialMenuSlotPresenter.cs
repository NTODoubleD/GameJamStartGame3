using System;
using Game.Gameplay.Items;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using UnityEngine;

namespace Game.UI
{
    public class RadialMenuSlotPresenter
    {
        private readonly GameItemInfo _item;
        private readonly ItemStorage _storage;

        public Sprite ItemIcon { get; private set; }
        public string ItemCount { get; private set; }

        public GameItemInfo Item => _item;
        
        public event Action<string> ItemCountChanged;

        public RadialMenuSlotPresenter(GameItemInfo item, ItemStorage storage)
        {
            _item = item;
            _storage = storage;
            ItemIcon = item.Icon;
            ItemCount = storage.GetCount(item).ToString();

            storage.ItemAdded += OnItemCountChanged;
            storage.ItemRemoved += OnItemCountChanged;
        }
        
        private void OnItemCountChanged(ItemInfo itemInfo, int _)
        {
            if (itemInfo != _item)
                return;
            
            ItemCount = _storage.GetCount(_item).ToString();
            ItemCountChanged?.Invoke(ItemCount);
        }
        
        ~RadialMenuSlotPresenter()
        {
            _storage.ItemAdded -= OnItemCountChanged;
            _storage.ItemRemoved -= OnItemCountChanged;
        }
    }
}