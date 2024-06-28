using System;
using System.Collections.Generic;
using System.Linq;
using DoubleDTeam.Extensions;
using Game.Infrastructure.Items;
using UnityEngine;

namespace Game.Infrastructure.Storage
{
    public class ItemStorage
    {
        private bool _enable;

        public event Action<bool> EnableStateChanged;

        public bool Enable
        {
            get => _enable;
            set
            {
                _enable = value;
                EnableStateChanged?.Invoke(_enable);
            }
        }

        private readonly List<Item> _items;

        public readonly ItemStorageInfo Info;

        public event Action<Item> ItemAdded;
        public event Action<Item> ItemPushed;
        public event Action<Item> ItemPopped;

        public int AmountItems => _items.Count;

        public IReadOnlyList<Item> Items => _items;

        public ItemStorage(ItemStorageInfo info)
        {
            _enable = true;

            Info = info;
            _items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            if (CanAddItem(item) == false)
                return;

            _items.Add(item);

            ItemAdded?.Invoke(item);
        }

        public void AddItems(IEnumerable<Item> items)
        {
            if (Enable == false)
                return;

            var arrayItems = items as Item[] ?? items.ToArray();

            if (CanAddItems(arrayItems) == false)
                return;

            foreach (var item in arrayItems)
                AddItem(item);
        }

        public void PushItem(Item item)
        {
            if (CanAddItem(item) == false)
                return;

            _items.Add(item);

            ItemPushed?.Invoke(item);
        }

        public void PushItems(IEnumerable<Item> items)
        {
            foreach (var item in items)
                PushItem(item);
        }

        public Item PopItem(string id, bool isReverseSearch = true)
        {
            if (Enable == false)
                return null;

            if (ContainsItem(id) == false)
            {
                Debug.LogError($"Item with ID {id} is not in inventory");
                return null;
            }

            var item = isReverseSearch
                ? _items.FirstFromEnd(i => i.Info.ID == id)
                : _items.First(i => i.Info.ID == id);

            _items.Remove(item);

            ItemPopped?.Invoke(item);

            return item;
        }

        public Item[] PopItems(IEnumerable<ItemsStack> items, bool isReverseSearch = true)
        {
            if (Enable == false)
                return Array.Empty<Item>();

            var stacks = items as ItemsStack[] ?? items.ToArray();

            if (ContainsItems(stacks) == false)
            {
                string errorText = stacks.Aggregate("The order cannot be completed:",
                    (current, itemsStack) => current + $"\n{itemsStack.ItemInfo.Name} {itemsStack.Amount}");

                Debug.LogError(errorText);
                return null;
            }

            int arrayLength = stacks.Sum(i => i.Amount);

            var result = new Item[arrayLength];
            int counter = 0;

            foreach (var itemsStack in stacks)
            {
                for (int i = 0; i < itemsStack.Amount; i++)
                {
                    result[counter] = PopItem(itemsStack.ItemInfo.ID, isReverseSearch);
                    counter++;
                }
            }

            return result;
        }

        public virtual bool CanAddItem(Item item)
        {
            if (Enable == false)
                return false;

            if (AmountItems + 1 > Info.Capacity)
                return false;

            if (Info.WhiteList.IsNullOrEmpty() == false)
                return Info.WhiteList.Contains(item.Info);

            if (Info.BlackList.IsNullOrEmpty() == false)
                return Info.BlackList.Contains(item.Info) == false;

            return true;
        }

        public bool CanAddItems(IEnumerable<Item> items)
        {
            if (Enable == false)
                return false;

            var itemsArray = items as Item[] ?? items.ToArray();

            if (AmountItems + itemsArray.Length > Info.Capacity)
                return false;

            foreach (var item in itemsArray)
            {
                if (CanAddItem(item) == false)
                    return false;
            }

            return true;
        }

        public bool ContainsItem(string id)
        {
            return _items.Contains(i => i.Info.ID == id);
        }

        public bool ContainsItems(IEnumerable<ItemsStack> items)
        {
            foreach (var itemsStack in items)
            {
                if (_items.Count(i => i.Info == itemsStack.ItemInfo) < itemsStack.Amount)
                    return false;
            }

            return true;
        }

        public int Counts(string id)
        {
            return _items.Count(i => i.Info.ID == id);
        }
    }
}