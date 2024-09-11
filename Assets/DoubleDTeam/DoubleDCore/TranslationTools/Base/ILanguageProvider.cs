using DoubleDCore.TranslationTools.Data;

namespace DoubleDCore.TranslationTools.Base
{
    public interface ILanguageProvider
    {
        public LanguageType GetLanguage();
    }
}