using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public abstract class LevelsConfig<T> : ScriptableObject
    {
        [SerializeField] private T[] _levelStats;

        public T GetStatsAt(int level)
        {
            int index = level - 1;
            index = Mathf.Clamp(index, 0, _levelStats.Length - 1);

            return _levelStats[index];
        }
    }
}