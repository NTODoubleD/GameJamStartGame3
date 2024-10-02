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

        public void Initialize(float startValue)
        {
            Refresh(startValue);
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
            _sliderImage.fillAmount = normalizedValue;
        }
    }
}