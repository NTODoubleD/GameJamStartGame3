using System;
using System.Collections.Generic;
using Game.Gameplay.Crafting;
using Game.Gameplay.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UICraftingRecepie : MonoBehaviour
    {
        private readonly List<GameObject> _resourceViews = new();
        
        [SerializeField] private Button _clickButton;
        [SerializeField] private UIResource _resourceViewPrefab;
        [SerializeField] private UICraftingArrow _arrowPrefab;
        [SerializeField] private Transform _resourcesRoot;

        private UICraftingArrow _arrowInstanced;
        
        public CraftingRecepie RecepieData { get; private set; }

        public event Action<CraftingRecepie> Clicked;

        private void OnEnable()
        {
            _clickButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _clickButton.onClick.RemoveListener(OnButtonClicked);
        }

        public void Init(CraftingRecepie recepie)
        {
            if (RecepieData != null)
            {
                for (int i = 0; i < _resourceViews.Count; i++)
                    Destroy(_resourceViews[i]);
                
                _resourceViews.Clear();
            }
            
            RecepieData = recepie;
            
            AddResourceRange(RecepieData.InputItems);

            _arrowInstanced = Instantiate(_arrowPrefab, _resourcesRoot);
            _arrowInstanced.Initialize(recepie.CraftTime);
            _resourceViews.Add(_arrowInstanced.gameObject);
            
            AddResourceRange(RecepieData.OutputItems);
        }

        private void AddResourceRange(IReadOnlyDictionary<GameItemInfo, int> data)
        {
            foreach (var (resource, count) in data)
            {
                var resourceView = Instantiate(_resourceViewPrefab, _resourcesRoot);
                resourceView.Initialize(resource, count);
                _resourceViews.Add(resourceView.gameObject);
            }
        }

        public void SetAvailable(bool isAvailable)
        {
            _clickButton.interactable = isAvailable;
            if(_arrowInstanced)
                _arrowInstanced.SetVisualActive(isAvailable);
        }

        private void OnButtonClicked()
        {
            Clicked?.Invoke(RecepieData);
        }
    }
}