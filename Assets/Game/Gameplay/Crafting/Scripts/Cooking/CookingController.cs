using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Items;
using Game.Infrastructure.Storage;
using UnityEngine;

namespace Game.Gameplay.Crafting
{
    public class CookingController
    {
        private readonly CookingConfig _config;
        private readonly CraftController _craftController;
        private readonly ItemStorage _itemStorage;
        private readonly CookingSlot[] _currentSlots;

        private int _nextFreeSlotIndex;
        private float _cookTimeLeft;
        private bool _isCooking;

        public event Action<int> Finished;
        public event Action FuelEnded;

        public CookingController(CookingConfig config, CraftController craftController, ItemStorage itemStorage)
        {
            _config = config;
            _craftController = craftController;
            _itemStorage = itemStorage;
            _currentSlots = new CookingSlot[config.CookingPlaceCount];
        }

        public IReadOnlyCollection<CraftingRecepie> GetRecepies() 
            => _config.Recepies;

        public int GetCookingPlaceCount() => _config.CookingPlaceCount;
        public GameItemInfo GetFuelItemInfo() => _config.GetFuelInfo().Item1;
        public int GetFuelAmount() => _itemStorage.GetCount(_config.GetFuelInfo().Item1);
        public bool CanAddFuelItem() => _itemStorage.GetCount(_config.GetFuelInfo().Item1) > 0;
        
        public void AddFuelItem()
        {
            _itemStorage.RemoveItems(_config.GetFuelInfo().Item1, 1);
            _cookTimeLeft += _config.GetFuelInfo().Item2;

            if (_isCooking == false)
                CookAsync().Forget();
        }
        
        public float GetTimeLeftForCooking() => _cookTimeLeft;

        public float GetCookingTimeLeft(int placeIndex) 
            => _currentSlots[placeIndex].TimeLeft;

        public float GetCookingTimeLeftPercentage(int placeIndex)
        {
            float currentTimeLeft = GetCookingTimeLeft(placeIndex);
            float totalTime = _currentSlots[placeIndex].Recepie.CraftTime;
            
            return currentTimeLeft / totalTime;
        }

        public bool CanAddToCooking(CraftingRecepie recepie)
        {
            if (_nextFreeSlotIndex >= _currentSlots.Length)
                return false;
            
            return _craftController.CanCraft(recepie, out int _);
        }

        public void AddToCooking(CraftingRecepie recepie)
        {
            foreach (var inputItem in recepie.InputItems)
                _itemStorage.RemoveItems(inputItem.Key, inputItem.Value);

            _currentSlots[_nextFreeSlotIndex].Recepie = recepie;
            _currentSlots[_nextFreeSlotIndex++].TimeLeft = recepie.CraftTime;
        }

        private async UniTask CookAsync()
        {
            _isCooking = true;
            
            while (_cookTimeLeft > 0)
            {
                await UniTask.DelayFrame(1);

                for (int i = 0; i < _currentSlots.Length; i++)
                {
                    if (_currentSlots[i].Recepie == null)
                        continue;
                    
                    var cookingSlot = _currentSlots[i];
                    
                    if (cookingSlot.TimeLeft <= 0)
                    {
                        foreach (var outputItem in cookingSlot.Recepie.OutputItems)
                            _itemStorage.AddItems(outputItem.Key, outputItem.Value);
                        
                        _currentSlots[i].Recepie = null;
                        Finished?.Invoke(i);
                    }
                }
                
                _cookTimeLeft -= Time.deltaTime;
            }
            
            for (int i = 0; i < _currentSlots.Length; i++)
            {
                if (_currentSlots[i].Recepie == null)
                    continue;
                    
                var cookingSlot = _currentSlots[i];
                
                foreach (var outputItem in cookingSlot.Recepie.InputItems)
                    _itemStorage.AddItems(outputItem.Key, outputItem.Value);

                _currentSlots[i].Recepie = null;
            }

            _isCooking = false;
            FuelEnded?.Invoke();
        }

        public class CookingSlot
        {
            public CraftingRecepie Recepie;
            public float TimeLeft;
        }
    }
}