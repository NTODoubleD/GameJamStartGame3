using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.Crafting;
using Game.Gameplay.SurvivalMechanics.Frost;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class CookingPage : MonoPage, IUIPage
    {
        private readonly List<UICraftingRecepie> _currentRecepieViews = new();
        
        [Header("UI Setup")]
        [SerializeField] private UICookingSlot[] _cookingSlots;
        [SerializeField] private UIFuelView _fuelView;
        [SerializeField] private Transform _recepiesRoot;
        
        [Header("Prefabs")]
        [SerializeField] private UICraftingRecepie _uiCraftingRecepiePrefab;

        private CookingController _cookingController;
        private GameInput _inputController;
        private FrostController _frostController;
        
        [Inject]
        private void Init(CookingController cookingController, 
            GameInput inputController, FrostController frostController)
        {
            _cookingController = cookingController;
            _inputController = inputController;
            _frostController = frostController;
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

                recepieView.Clicked += OnRecepieClicked;
                
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
                
                _cookingSlots[i].PickRequested += OnMealPickRequested;
            }
            
            UpdateFuelButton();
            _fuelView.SetFuelResourceInfo(_cookingController.GetFuelItemInfo(), _cookingController.GetFuelAmount(), 1);

            _fuelView.AddFuelRequested += OnAddFuelRequested;
            _cookingController.Interrupted += OnSlotInterrupted;
            _frostController.FrostLevelChanged += OnFrostLevelChanged;
            
            SetCanvasState(true);
            UpdateAsync().Forget();
        }

        private async UniTask UpdateAsync()
        {
            while (PageIsDisplayed)
            {
                _fuelView.SetTimeLeft(_cookingController.CookTimeLeft);
                UpdateCookingSlotsState();
                UpdateRecepiesAvailable();

                await UniTask.DelayFrame(1);
            }
        }

        private void UpdateFuelButton()
        {
            bool isInteractable = _cookingController.CanAddFuelItem(out string errorText);
            _fuelView.SetFuelButtonInfo(isInteractable, errorText);
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

        private void UpdateRecepiesAvailable()
        {
            foreach (var recepieView in _currentRecepieViews)
                recepieView.SetAvailable(_cookingController.CanAddToCooking(recepieView.RecepieData));
        }

        public override void Close()
        {
            _fuelView.AddFuelRequested -= OnAddFuelRequested;
            _cookingController.Interrupted -= OnSlotInterrupted;
            _frostController.FrostLevelChanged -= OnFrostLevelChanged;

            foreach (var recepieView in _currentRecepieViews)
                recepieView.Clicked -= OnRecepieClicked;
            
            for (int i = 0; i < _cookingSlots.Length; i++)
                _cookingSlots[i].PickRequested -= OnMealPickRequested;
            
            SetCanvasState(false);
            
            _inputController.UI.Disable();
            _inputController.Player.Enable();
        }

        private void OnAddFuelRequested()
        {
            if (_cookingController.CanAddFuelItem(out string _) == false)
                throw new Exception("CAN'T ADD FUEL ITEM");
            
            _cookingController.AddFuelItem();
            UpdateFuelButton();
            _fuelView.SetTimeLeft(_cookingController.CookTimeLeft);
            _fuelView.SetFuelResourceInfo(_cookingController.GetFuelItemInfo(), _cookingController.GetFuelAmount(), 1);
            
            UpdateRecepiesAvailable();
        }

        private void OnFrostLevelChanged(FrostLevel obj)
        {
            UpdateFuelButton();
        }

        private void OnSlotInterrupted(int slotIndex)
        {
            _cookingSlots[slotIndex].Clear();
        }

        private void OnRecepieClicked(CraftingRecepie recepie)
        {
            if (_cookingController.CanAddToCooking(recepie) == false)
                throw new ArgumentException("CAN'T COOK THIS RECEPIE");

            int slotIndex = _cookingController.AddToCooking(recepie);
            _cookingSlots[slotIndex].Init(recepie, recepie.CraftTime);
            
            UpdateRecepiesAvailable();
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
            
            UpdateRecepiesAvailable();
        }
    }
}