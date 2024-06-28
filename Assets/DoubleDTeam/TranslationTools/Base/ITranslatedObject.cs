using DoubleDTeam.TranslationTools.Data;

namespace DoubleDTeam.TranslationTools.Base
{
    public interface ITranslatedObject
    {
        public void OnLanguageChanged(LanguageType languageType);
    }
}