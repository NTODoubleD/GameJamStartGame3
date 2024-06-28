using DoubleDTeam.Containers.Base;
using DoubleDTeam.TranslationTools.Data;

namespace DoubleDTeam.TranslationTools.Base
{
    public interface ILanguageProvider : IModule
    {
        public LanguageType GetLanguage();
    }
}