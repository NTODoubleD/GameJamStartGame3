using DoubleDCore.TranslationTools.Base;
using DoubleDCore.TranslationTools.Data;

namespace DoubleDCore.TranslationTools
{
    public class MockLanguageProvider : ILanguageProvider
    {
        public LanguageType GetLanguage()
        {
            return LanguageType.Ru;
        }
    }
}