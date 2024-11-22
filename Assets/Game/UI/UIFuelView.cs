using System;
using System.Collections.Generic;
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

        [SerializeField] private Image _progressBar;

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
            _cachedTimeLeft = TimeSpan.FromSeconds(timeLeft);

            UpdateTime(timeLeft);
        }

        private void OnButtonClicked()
        {
            AddFuelRequested?.Invoke();
        }

        public void UpdateTime(float time)
        {
            _emptyCampVisual.SetActive(!(time > 0));
            _firingCampVisual.SetActive(time > 0);

            var tempTime = TimeSpan.FromSeconds(time);

            _timeLeft.text = $"{tempTime.Minutes:D1}:{tempTime.Seconds:D2}";

            _progressBar.fillAmount = time / (float)_cachedTimeLeft.TotalSeconds;
        }
    }
}