using DoubleDCore.TranslationTools.Base;
using DoubleDCore.TranslationTools.Data;
using UnityEngine;

namespace DoubleDCore.TranslationTools
{
    public class PCLanguageProvider : ILanguageProvider
    {
        public LanguageType GetLanguage()
        {
            return LanguageType.Ru;
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                    return LanguageType.Ru;
                default:
                    return LanguageType.En;
            }
        }
    }
}