using System;
using Game.Gameplay.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIFuelView : MonoBehaviour
    {
        [SerializeField] private Button _addFuelButton;
        [SerializeField] private TMP_Text _fuelButtonText;
        [SerializeField] private TMP_Text _timeLeft;
        [SerializeField] private Image _resourceIcon;
        [SerializeField] private TMP_Text _resourceCount;

        private TimeSpan _cachedTimeLeft;
        private string _fuelButtonDefaultText;
        
        public event Action AddFuelRequested;

        private void Awake()
        {
            _fuelButtonDefaultText = _fuelButtonText.text;
        }

        private void OnEnable()
        {
            _addFuelButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _addFuelButton.onClick.RemoveListener(OnButtonClicked);
        }

        public void SetFuelButtonInfo(bool isInteractable, string text = null)
        {
            _addFuelButton.interactable = isInteractable;
            _fuelButtonText.text = text == null ? _fuelButtonDefaultText : text;
        }

        public void SetTimeLeft(float timeLeft)
        {
            _cachedTimeLeft = TimeSpan.FromSeconds(timeLeft);
            _timeLeft.text = $"{_cachedTimeLeft.Minutes:D1}:{_cachedTimeLeft.Seconds:D2}";
        }

        public void SetFuelResourceInfo(GameItemInfo resource, int storageCount, int useCount)
        {
            _resourceIcon.sprite = resource.Icon;
            _resourceCount.text = $"{storageCount}/{useCount}";
        }

        private void OnButtonClicked()
        {
            AddFuelRequested?.Invoke();
        }
    }
}