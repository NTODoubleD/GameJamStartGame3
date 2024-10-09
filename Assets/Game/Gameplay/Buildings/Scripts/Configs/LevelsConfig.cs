using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public interface ILevelsConfig
    {
        string Description { get; }
        string GetStatsNumericValue(int level);
        bool CanShowStatsNumericValue();
    }
    
    public abstract class LevelsConfig<T> : ScriptableObject, ILevelsConfig
    {
        [SerializeField] private T[] _levelStats;
        [SerializeField] private TranslatedText _description;
        
        public string Description => _description.GetText();

        public T GetStatsAt(int level)
        {
            int index = level - 1;
            index = Mathf.Clamp(index, 0, _levelStats.Length - 1);

            return _levelStats[index];
        }

        public abstract string GetStatsNumericValue(int level);
        public bool CanShowStatsNumericValue() => true;
    }
}