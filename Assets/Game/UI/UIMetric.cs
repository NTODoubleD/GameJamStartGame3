using System;
using DoubleDCore.Tween.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIMetric : MonoBehaviour
    {
        [SerializeField] private Image _sliderImage;
        [SerializeField] private UIFlickeringEffect _effect;
        [SerializeField] private Image _alert;
        
        [Header("Slider Settings")]
        [SerializeField] private float _sliderSpeed = 0.1f;
        [SerializeField] private SliderUpdateType _updateType;

        private float _targetValue;

        public void Initialize(float startValue)
        {
            Refresh(startValue);
        }

        private void Awake()
        {
            _sliderImage.fillAmount = 1;
            _targetValue = _sliderImage.fillAmount;
        }

        private void Update()
        {
            switch (_updateType)
            {
                case SliderUpdateType.Lerp:
                    _sliderImage.fillAmount =
                        Mathf.Lerp(_sliderImage.fillAmount, _targetValue, _sliderSpeed * Time.deltaTime);
                    break;
                case SliderUpdateType.MoveTowards:
                    _sliderImage.fillAmount =
                        Mathf.MoveTowards(_sliderImage.fillAmount, _targetValue, _sliderSpeed * Time.deltaTime);
                    break;
            }
        }

        public void Refresh(float newValue)
        {
            SetSlider(newValue);

            if (newValue < 30)
                _effect.StartAnimation();
            else if (_effect.IsActive)
                _effect.StopAnimation();
            
            _alert.gameObject.SetActive(Mathf.Approximately(newValue, 0));
        }

        private void SetSlider(float startValue)
        {
            var normalizedValue = Mathf.Clamp01(startValue / 100);
            _targetValue = normalizedValue;
        }
        
        private enum SliderUpdateType
        {
            Lerp,
            MoveTowards
        }
    }
}