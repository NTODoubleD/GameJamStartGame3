using System;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Crafting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UICookingSlot : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Button _pickButton;

        [Header("Colors")] 
        [SerializeField] private Color _defaultProgressColor;
        [SerializeField] private Color _completedProgressColor;

        private TimeSpan _cachedTimeLeft;
        private float _targetAmount;

        [Header("Ready|Unready")] 
        [SerializeField] private UIResource ReadyResourceView;
        [SerializeField] private UIResource UnReadyResourceView;
        
        public CraftingRecepie CurrentRecepie { get; private set; }
        
        public event Action<UICookingSlot> PickRequested;

        private void OnEnable()
        {
            _pickButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _pickButton.onClick.RemoveListener(OnButtonClicked);
        }

        public void Init(CraftingRecepie recepie, float timeLeft)
        {
            CurrentRecepie = recepie;

            var mealItem = recepie.OutputItems.Keys.First();
            int mealCount = recepie.OutputItems[mealItem];
            
            var rawItem = recepie.InputItems.Count > 0 ?  recepie.InputItems.Keys.First() : mealItem;
            var rawCount =  recepie.InputItems.Count > 0 ?  recepie.InputItems[rawItem] : mealCount;

            ReadyResourceView.Initialize(mealItem, mealCount);
            UnReadyResourceView.Initialize(rawItem, rawCount);

            ReadyResourceView.gameObject.SetActive(true);
            UnReadyResourceView.gameObject.SetActive(true);

            Refresh(timeLeft);
        }

        public void Refresh(float timeLeft)
        {
            float progressPart = 1 - timeLeft / CurrentRecepie.CraftTime;
            _progressBar.fillAmount = progressPart;

            _cachedTimeLeft = TimeSpan.FromSeconds(timeLeft);
            _progressText.text = $"{_cachedTimeLeft.Minutes:D1}:{_cachedTimeLeft.Seconds:D2}";

            _progressBar.color = Mathf.Approximately(timeLeft, 0) ? _completedProgressColor : _defaultProgressColor;
            
            ReadyResourceView.gameObject.SetActive(Mathf.Approximately(timeLeft, 0));
            UnReadyResourceView.gameObject.SetActive(!Mathf.Approximately(timeLeft, 0));
        }

        public void Clear()
        {            
            ReadyResourceView.gameObject.SetActive(false);
            UnReadyResourceView.gameObject.SetActive(false);
            
            _progressText.text = string.Empty;
            _progressBar.fillAmount = 0;
            CurrentRecepie = null;
        }

        private void OnButtonClicked()
        {
            PickRequested?.Invoke(this);
        }
    }
}