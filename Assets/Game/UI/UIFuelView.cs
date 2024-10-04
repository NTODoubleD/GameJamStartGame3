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
        [SerializeField] private TMP_Text _timeLeft;
        [SerializeField] private Image _resourceIcon;
        [SerializeField] private TMP_Text _resourceCount;

        private TimeSpan _cachedTimeLeft;
        
        public event Action AddFuelRequested;

        private void OnEnable()
        {
            _addFuelButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _addFuelButton.onClick.RemoveListener(OnButtonClicked);
        }

        public void SetButtonInteractable(bool isInteractable)
        {
            _addFuelButton.interactable = isInteractable;
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