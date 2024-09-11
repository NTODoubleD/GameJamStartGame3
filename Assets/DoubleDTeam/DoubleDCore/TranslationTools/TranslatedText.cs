using System;
using System.Collections.Generic;
using DoubleDCore.TranslationTools.Data;
using UnityEngine;

namespace DoubleDCore.TranslationTools
{
    [Serializable]
    public class TranslatedText
    {
        [TextArea, SerializeField] private string _ru = "no translation";
        [TextArea, SerializeField] private string _en = "no translation";
        //[TextArea, SerializeField] private string _tr = "no translation";

        public TranslatedText()
        {
        }

        public TranslatedText(string ru, string en)
        {
            _ru = ru;
            _en = en;
        }

        public string Ru => _ru;
        public string En => _en;

        public IReadOnlyDictionary<LanguageType, string> Translation => new Dictionary<LanguageType, string>()
        {
            { LanguageType.Ru, _ru },
            { LanguageType.En, _en }
        };
    }
}