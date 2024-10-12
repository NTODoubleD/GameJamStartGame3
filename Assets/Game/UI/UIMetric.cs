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

        [Header("Slider Settings")] [SerializeField]
        private float _sliderSpeed = 0.1f;

        [SerializeField] private SliderUpdateType _updateType;
        [SerializeField] private float _impactThreshold = 30;
        [SerializeField] private Gradient _impactColor;

        private float _targetValue;

        public void Initialize(float startValue)
        {
            Refresh(startValue, true);
        }

        private void Awake()
        {
            _sliderImage.fillAmount = 1;
            _targetValue = _sliderImage.fillAmount;
        }

        private float _oldValue = 0;

        private void Update()
        {
            float lerpValue = _updateType switch
            {
                SliderUpdateType.Lerp => Mathf.Lerp(_sliderImage.fillAmount, _targetValue,
                    _sliderSpeed * Time.deltaTime),

                SliderUpdateType.MoveTowards => Mathf.MoveTowards(_sliderImage.fillAmount, _targetValue,
                    _sliderSpeed * Time.deltaTime),

                _ => 0f
            };

            _sliderImage.fillAmount = lerpValue;

            float delta = lerpValue - _oldValue;
            _oldValue = lerpValue;

            delta = Mathf.Clamp(delta, -_impactThreshold, _impactThreshold);

            float progress = (delta + _impactThreshold) / (_impactThreshold * 2);

            _sliderImage.color = _impactColor.Evaluate(progress);
        }

        public void Refresh(float newValue, bool force = false)
        {
            SetSlider(newValue, force);

            if (newValue < 30)
                _effect.StartAnimation();
            else if (_effect.IsActive)
                _effect.StopAnimation();

            _alert.gameObject.SetActive(Mathf.Approximately(newValue, 0));
        }

        private void SetSlider(float startValue, bool force)
        {
            var normalizedValue = Mathf.Clamp01(startValue / 100);
            _targetValue = normalizedValue;

            if (force)
                _sliderImage.fillAmount = _targetValue;
        }

        private enum SliderUpdateType
        {
            Lerp,
            MoveTowards
        }
    }
}