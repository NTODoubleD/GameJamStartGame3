using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "TownHall Levels Config", menuName = "Buildings/TownHall Levels")]
    public class TownHallLevelsConfig : ScriptableObject, ILevelsConfig
    {
        [SerializeField] private TranslatedText _description;

        public string Description => _description.GetText();
        
        public string GetStatsNumericValue(int level)
        {
            return string.Empty;
        }

        public bool CanShowStatsNumericValue()
        {
            return false;
        }
    }
}