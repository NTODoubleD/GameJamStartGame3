using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class StatusSliderView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _lowValueIcon;
        [SerializeField] private Image _highValueIcon;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _sliderImage;
        
        [Header("Slider Colors")]
        [SerializeField] private Color _badColor = Color.red;
        [SerializeField] private Color _goodColor = Color.green;

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public void SetSliderState(float value)
        {
            Debug.Log($"Slider = {value} Color = {value}");
            
            _slider.value = Mathf.Lerp(_slider.minValue, _slider.maxValue, value);
            
            Color newColor = Color.Lerp(_badColor, _goodColor, value);
            _sliderImage.color = newColor;
        }
    }
}