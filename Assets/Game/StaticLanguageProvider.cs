using DoubleDCore.TranslationTools.Data;
using UnityEngine;

namespace Game
{
    public static class StaticLanguageProvider
    {
        public static LanguageType GetLanguage()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.English:
                    return LanguageType.En;
                case SystemLanguage.Russian:
                    return LanguageType.Ru;
                default:
                    return LanguageType.En;
            }
        }
    }
}