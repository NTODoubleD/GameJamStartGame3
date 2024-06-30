using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIResourceProperty : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _resourceNameText;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _counterText;

        [SerializeField] private bool _useStaticName;

        [SerializeField, ShowIf("_useStaticName")]
        private string _name;

        public event Action<UIResourceProperty> ValueChanged;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(UpdateCounter);
            _slider.onValueChanged.AddListener(CallValueChange);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(UpdateCounter);
            _slider.onValueChanged.RemoveListener(CallValueChange);
        }

        public void Refresh(string resourceName, int maxLevel)
        {
            _resourceNameText.text = _useStaticName ? _name : resourceName;
            _slider.maxValue = maxLevel;

            UpdateCounter(_slider.value);
        }

        public void ChangeValue(int value)
        {
            _slider.value = value;
        }

        public int GetResourceAmount()
            => Mathf.RoundToInt(_slider.value);

        private void UpdateCounter(float value)
        {
            _counterText.text = Mathf.RoundToInt(value).ToString();
        }

        private void CallValueChange(float value)
        {
            ValueChanged?.Invoke(this);
        }
    }
}