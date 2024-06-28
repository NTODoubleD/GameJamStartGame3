using System;
using DoubleDTeam.TranslationTools.Data;

namespace DoubleDTeam.TranslationTools.Extensions
{
    public static class TranslationToolsExtensions
    {
        public static LanguageMask ToLanguageMask(this LanguageType type)
        {
            return type switch
            {
                LanguageType.Ru => LanguageMask.Ru,
                LanguageType.En => LanguageMask.En,
                //LanguageTypes.Tr => LanguageMask.Tr,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static LanguageMask ToLanguageMask(this string code)
        {
            return code switch
            {
                LanguageStrings.RuMark => LanguageMask.Ru,
                LanguageStrings.EnMark => LanguageMask.En,
                //LanguageStrings.TrMark => LanguageMask.Tr,
                _ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
            };
        }

        public static LanguageType ToLanguageType(this string code)
        {
            return code switch
            {
                LanguageStrings.RuMark => LanguageType.Ru,
                LanguageStrings.EnMark => LanguageType.En,
                //LanguageStrings.TrMark => LanguageTypes.Tr,
                _ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
            };
        }
    }
}