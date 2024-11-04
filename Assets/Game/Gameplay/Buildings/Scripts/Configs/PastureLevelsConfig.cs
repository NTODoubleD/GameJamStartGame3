using System;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "Pasture Config", menuName = "Buildings/Pasture Config")]
    public class PastureLevelsConfig : LevelsConfig<PastureLevelStat>
    {
        public override string GetStatsNumericValue(int level)
        {
            var stat = GetStatsAt(level);
            return stat.Capacity.ToString();
        }
    }

    [Serializable]
    public class PastureLevelStat
    {
        [SerializeField] private int _capacity;
        [SerializeField] private Vector3 _centerPoint;
        [SerializeField] private float _width;
        
        public Vector3 CenterPoint => _centerPoint;
        public float Width => _width;
        public int Capacity => _capacity;
    }
}