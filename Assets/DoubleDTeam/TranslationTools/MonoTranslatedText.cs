using DoubleDTeam.TranslationTools.Base;
using DoubleDTeam.TranslationTools.Data;
using TMPro;
using UnityEngine;

namespace DoubleDTeam.TranslationTools
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MonoTranslatedText : MonoBehaviour, ITranslatedObject
    {
        [SerializeField] private TranslatedText _text;

        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        public void InsertText(params object[] strings)
        {
            if (_textMeshPro == null)
                return;

            _textMeshPro.text = string.Format(_text.Text, strings);
        }

        public void ChangeText(TranslatedText text)
        {
            _text = text;
            _textMeshPro.text = _text.Text;
        }

        public void OnLanguageChanged(LanguageType languageType)
        {
            _textMeshPro.text = _text.Text;
        }
    }
}