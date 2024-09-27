using Game.Infrastructure.Storage;
using UnityEngine;

namespace Game.Gameplay.Crafting
{
    public class CraftController
    {
        private readonly ItemStorage _storage;

        public CraftController(ItemStorage storage)
        {
            _storage = storage;
        }
        
        public bool CanCraft(CraftingRecepie recepie, out int craftTimes)
        {
            craftTimes = int.MaxValue;
            
            foreach (var item in recepie.InputItems)
            {
                int storageCount = _storage.GetCount(item.Key);
                craftTimes = Mathf.Min(craftTimes, storageCount / item.Value);
                
                if (craftTimes == 0)
                    return false;
            }

            return true;
        }

        public void Craft(CraftingRecepie recepie, int craftTimes)
        {
            foreach (var item in recepie.InputItems)
                _storage.RemoveItems(item.Key, item.Value * craftTimes);
            
            foreach (var item in recepie.OutputItems)
                _storage.AddItems(item.Key, item.Value * craftTimes);
        }
    }
}