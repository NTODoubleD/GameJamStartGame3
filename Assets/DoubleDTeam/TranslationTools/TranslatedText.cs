using System;
using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.TranslationTools.Base;
using DoubleDTeam.TranslationTools.Data;
using UnityEngine;

namespace DoubleDTeam.TranslationTools
{
    [Serializable]
    public class TranslatedText
    {
        [TextArea, SerializeField] private string _ru = "no translation";
        [TextArea, SerializeField] private string _en = "no translation";
        //[TextArea, SerializeField] private string _tr = "no translation";

        public string Text => GetTranslationText();

        public IReadOnlyDictionary<LanguageType, string> Translation => new Dictionary<LanguageType, string>()
        {
            { LanguageType.Ru, _ru },
            { LanguageType.En, _en }
        };

        private string GetTranslationText() =>
            Translation[/*Services.ProjectContext.GetModule<ILanguageProvider>().GetLanguage()*/LanguageType.Ru];
    }
}