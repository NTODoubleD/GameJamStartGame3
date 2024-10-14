using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Items;
using Game.Gameplay.SurvivalMechanics.Frost;
using Game.Infrastructure.Storage;
using UnityEngine;

namespace Game.Gameplay.Crafting
{
    public class CookingController
    {
        private readonly CookingConfig _config;
        private readonly CraftController _craftController;
        private readonly ItemStorage _itemStorage;
        private readonly FrostController _frostController;
        private readonly CookingSlot[] _currentSlots;
        
        private int _nextFreeSlotIndex;
        private bool _isCooking;
        
        public float CookTimeLeft { get; private set; }

        public event Action<int> Interrupted;
        public event Action CookingEnded;
        public event Action CookingStarted;

        public CookingController(CookingConfig config, CraftController craftController,
            ItemStorage itemStorage, FrostController frostController)
        {
            _config = config;
            _craftController = craftController;
            _itemStorage = itemStorage;
            _frostController = frostController;

            _currentSlots = new CookingSlot[config.CookingPlaceCount];

            for (int i = 0; i < config.CookingPlaceCount; i++)
                _currentSlots[i] = new CookingSlot();
        }

        public IReadOnlyCollection<CraftingRecepie> GetRecepies() 
            => _config.Recepies;

        public int GetCookingPlaceCount() => _config.CookingPlaceCount;
        public GameItemInfo GetFuelItemInfo() => _config.GetFuelInfo().Item1;
        public int GetFuelAmount() => _itemStorage.GetCount(_config.GetFuelInfo().Item1);
        
        public bool CanAddFuelItem(out string errorText)
        {
            errorText = null;
            
            if (_itemStorage.GetCount(_config.GetFuelInfo().Item1) == 0)
            {
                errorText = "Нету ресурса";
                return false;
            }

            if (_frostController.CurrentFrostLevel == FrostLevel.Strong)
            {
                errorText = "Идёт буря";
                return false;
            }

            return true;
        }

        public void AddFuelItem()
        {
            _itemStorage.RemoveItems(_config.GetFuelInfo().Item1, 1);
            CookTimeLeft += _config.GetFuelInfo().Item2;

            if (_isCooking == false)
                CookAsync().Forget();
        }

        public void StopCookingForced()
        {
            CookTimeLeft = 0;
        }

        public float GetCookingTimeLeft(int placeIndex) 
            => _currentSlots[placeIndex].TimeLeft;
        
        public CraftingRecepie GetCookingSlotCurrentRecepie(int slotIndex) 
            => _currentSlots[slotIndex].Recepie;

        public bool CanAddToCooking(CraftingRecepie recepie)
        {
            if (_nextFreeSlotIndex >= _currentSlots.Length)
                return false;

            if (CookTimeLeft <= 0)
                return false;
            
            return _craftController.CanCraft(recepie, out int _);
        }

        public int AddToCooking(CraftingRecepie recepie)
        {
            foreach (var inputItem in recepie.InputItems)
                _itemStorage.RemoveItems(inputItem.Key, inputItem.Value);

            _currentSlots[_nextFreeSlotIndex].Recepie = recepie;
            _currentSlots[_nextFreeSlotIndex].TimeLeft = recepie.CraftTime;
            
            int currentFreeSlotIndex = _nextFreeSlotIndex;
            SetNextFreeSlot();
            return currentFreeSlotIndex;
        }

        public void TakeCookedRecepie(int placeIndex)
        {
            foreach (var outputItem in _currentSlots[placeIndex].Recepie.OutputItems)
                _itemStorage.AddItems(outputItem.Key, outputItem.Value);
            
            _currentSlots[placeIndex].Recepie = null;
            SetNextFreeSlot();
        }
        
        private async UniTask CookAsync()
        {
            _isCooking = true;
            CookingStarted?.Invoke();
            
            while (CookTimeLeft > 0)
            {
                await UniTask.DelayFrame(1);

                for (int i = 0; i < _currentSlots.Length; i++)
                {
                    if (_currentSlots[i].Recepie == null || _currentSlots[i].TimeLeft <= 0)
                        continue;
                    
                    var cookingSlot = _currentSlots[i];
                    cookingSlot.TimeLeft = Mathf.Max(0, cookingSlot.TimeLeft - Time.deltaTime);
                }
                
                CookTimeLeft -= Time.deltaTime;
            }
            
            for (int i = 0; i < _currentSlots.Length; i++)
            {
                if (_currentSlots[i].Recepie == null || _currentSlots[i].TimeLeft <= 0)
                    continue;
                    
                var cookingSlot = _currentSlots[i];
                
                foreach (var inputItem in cookingSlot.Recepie.InputItems)
                    _itemStorage.AddItems(inputItem.Key, inputItem.Value);

                _currentSlots[i].Recepie = null;
                Interrupted?.Invoke(i);
            }
            
            SetNextFreeSlot();

            _isCooking = false;
            CookingEnded?.Invoke();
        }

        private void SetNextFreeSlot()
        {
            for (int i = 0; i < _currentSlots.Length; i++)
            {
                if (_currentSlots[i].Recepie == null)
                {
                    _nextFreeSlotIndex = i;
                    return;
                }
            }
            
            _nextFreeSlotIndex = _currentSlots.Length;
        }

        public class CookingSlot
        {
            public CraftingRecepie Recepie;
            public float TimeLeft;
        }
    }
}