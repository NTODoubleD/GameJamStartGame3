using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.Crafting;
using ModestTree;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Pages
{
    public class CookingPage : MonoPage, IUIPage
    {
        private readonly List<UICraftingRecepie> _currentRecepieViews = new();
        
        [Header("UI Setup")]
        [SerializeField] private UICookingSlot[] _cookingSlots;
        [SerializeField] private Button _addFuelButton;
        [SerializeField] private UIFuelView _fuelView;
        [SerializeField] private Transform _recepiesRoot;
        
        [Header("Prefabs")]
        [SerializeField] private UICraftingRecepie _uiCraftingRecepiePrefab;

        private CookingController _cookingController;
        
        [Inject]
        private void Init(CookingController cookingController)
        {
            _cookingController = cookingController;
        }

        public void Open()
        {
            for (int i = 0; i < _currentRecepieViews.Count; i++)
                Destroy(_currentRecepieViews[i].gameObject);
            
            _currentRecepieViews.Clear();

            foreach (var recepie in _cookingController.GetRecepies())
            {
                var recepieView = Instantiate(_uiCraftingRecepiePrefab, _recepiesRoot);
                recepieView.Init(recepie);
                recepieView.SetAvailable(_cookingController.CanAddToCooking(recepie));
                _currentRecepieViews.Add(recepieView);
            }

            for (int i = 0; i < _cookingController.GetCookingPlaceCount(); i++)
            {
                var slotRecepie = _cookingController.GetCookingSlotCurrentRecepie(i);

                if (slotRecepie != null)
                {
                    float timeLeft = _cookingController.GetCookingTimeLeft(i);
                    _cookingSlots[i].Init(slotRecepie, timeLeft);
                }
                else
                {
                    _cookingSlots[i].Clear();
                }
            }
            
            _fuelView.SetButtonInteractable(_cookingController.CanAddFuelItem());
            
            UpdateAsync().Forget();
        }

        private async UniTask UpdateAsync()
        {
            while (PageIsDisplayed)
            {
                _fuelView.SetTimeLeft(_cookingController.CookTimeLeft);
                UpdateCookingSlotsState();

                await UniTask.Delay(1000);
            }
        }

        private void UpdateCookingSlotsState()
        {
            for (int i = 0; i < _cookingController.GetCookingPlaceCount(); i++)
            {
                if (_cookingSlots[i].CurrentRecepie != null)
                {
                    float timeLeft = _cookingController.GetCookingTimeLeft(i);
                    _cookingSlots[i].Refresh(timeLeft);
                }
            }
        }

        public override void Close()
        {
            
        }

        private void OnRecepieClicked(CraftingRecepie recepie)
        {
            if (_cookingController.CanAddToCooking(recepie) == false)
                throw new ArgumentException("CAN'T COOK THIS RECEPIE");

            int slotIndex = _cookingController.AddToCooking(recepie);
            _cookingSlots[slotIndex].Init(recepie, recepie.CraftTime);
        }

        private void OnMealPickRequested(UICookingSlot slot)
        {
            var recepie = slot.CurrentRecepie;
            
            if (recepie == null)
                return;

            int slotIndex = _cookingSlots.IndexOf(slot);
            
            if (_cookingController.GetCookingTimeLeft(slotIndex) > 0)
                return;
            
            _cookingController.TakeCookedRecepie(slotIndex);
            slot.Clear();
        }
    }
}