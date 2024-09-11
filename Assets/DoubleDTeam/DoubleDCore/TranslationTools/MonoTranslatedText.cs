using DoubleDCore.TranslationTools.Extensions;
using TMPro;
using UnityEngine;

namespace DoubleDCore.TranslationTools
{
    [RequireComponent(typeof(TMP_Text))]
    public class MonoTranslatedText : MonoBehaviour
    {
        [SerializeField] private TranslatedText _text;

        private TMP_Text _textMeshPro;

        private void Awake()
        {
            _textMeshPro = GetComponent<TMP_Text>();
            _textMeshPro.text = _text.GetText();
        }

        public void InsertText(params object[] strings)
        {
            if (_textMeshPro == null)
                return;

            _textMeshPro.text = string.Format(_text.GetText(), strings);
        }

        public void ChangeText(TranslatedText text)
        {
            _text = text;
            _textMeshPro.text = _text.GetText();
        }
    }
}