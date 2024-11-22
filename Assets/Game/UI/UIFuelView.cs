using System;
using System.Collections.Generic;
using Game.Gameplay.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIFuelView : MonoBehaviour
    {
        [SerializeField] private List<Button> _addFuelButtons;
        [SerializeField] private TMP_Text _timeLeft;

        [SerializeField] private GameObject _emptyCampVisual;
        [SerializeField] private GameObject _firingCampVisual;
        
        private TimeSpan _cachedTimeLeft;
        private string _fuelButtonDefaultText;
        
        public event Action AddFuelRequested;


        private void OnEnable()
        {
            foreach (var button in _addFuelButtons) 
                button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            foreach (var button in _addFuelButtons) 
                button.onClick.RemoveListener(OnButtonClicked);
        }

        public void SetFuelButtonInfo(bool isInteractable, string text = null)
        {
            foreach (var button in _addFuelButtons) 
                button.interactable = isInteractable;
        }

        public void SetTimeLeft(float timeLeft)
        {
            _emptyCampVisual.SetActive(!(timeLeft > 0));
            _firingCampVisual.SetActive(timeLeft > 0);
            
            _cachedTimeLeft = TimeSpan.FromSeconds(timeLeft);
            _timeLeft.text = $"{_cachedTimeLeft.Minutes:D1}:{_cachedTimeLeft.Seconds:D2}";
        }

        private void OnButtonClicked()
        {
            AddFuelRequested?.Invoke();
        }
    }
}