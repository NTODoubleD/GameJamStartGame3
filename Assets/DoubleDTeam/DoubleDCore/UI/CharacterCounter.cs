using TMPro;
using UnityEngine;

namespace DoubleDCore.UI
{
    public class CharacterCounter : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string _format = "({0}/{1})";
        [SerializeField] private Color32 _normalColor;
        [SerializeField] private Color32 _warningColor;

        private void OnEnable()
        {
            _inputField.onValueChanged.AddListener(OnValueChanged);
            OnValueChanged(_inputField.text);
        }

        private void OnDisable()
        {
            _inputField.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(string text)
        {
            _text.text = string.Format(_format, text.Length, _inputField.characterLimit);
            _text.color = text.Length == _inputField.characterLimit ? _warningColor : _normalColor;
        }
    }
}