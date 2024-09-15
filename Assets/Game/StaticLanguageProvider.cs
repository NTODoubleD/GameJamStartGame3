using DoubleDCore.TranslationTools.Data;
using UnityEngine;

namespace Game
{
    public static class StaticLanguageProvider
    {
        public static LanguageType GetLanguage()
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