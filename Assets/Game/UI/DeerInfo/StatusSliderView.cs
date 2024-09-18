using System;
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
        [SerializeField]
        private SliderColor[] _colors = new[]
        {
            new SliderColor(0, Color.clear),
            new SliderColor(1, Color.clear),
            new SliderColor(2, Color.clear),
            new SliderColor(3, Color.clear),
        };

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public void SetSliderState(float value)
        {
            _slider.value = Mathf.Lerp(_slider.minValue, _slider.maxValue, value);

            foreach (var color in _colors)
                if ((int)_slider.value == color.Value)
                    _sliderImage.color = color.Color;
        }

        [Serializable]
        private struct SliderColor
        {
            public int Value;
            public Color Color;

            public SliderColor(int val, Color color)
            {
                Value = val;
                Color = color;
            }
        }
    }
}