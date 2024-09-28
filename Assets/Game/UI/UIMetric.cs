using DoubleDCore.Tween.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIMetric : MonoBehaviour
    {
        [SerializeField] private Image _sliderImage;
        [SerializeField] private Image _icon;
        [SerializeField] private UIFlickeringEffect _effect;

        public void Initialize(Sprite icon, float startValue)
        {
            _icon.sprite = icon;
            Refresh(startValue);
        }

        public void Refresh(float newValue)
        {
            SetSlider(newValue);

            if (newValue < 30)
                _effect.StartAnimation();
            else if (_effect.IsActive)
                _effect.StopAnimation();
        }

        private void SetSlider(float startValue)
        {
            var normalizedValue = Mathf.Clamp01(startValue / 100);
            _sliderImage.fillAmount = normalizedValue;
        }
    }
}