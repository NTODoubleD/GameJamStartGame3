using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "Pasture Config", menuName = "Buildings/Pasture Config")]
    public class PastureLevelsConfig : LevelsConfig<PastureLevelStat>
    {
        public override string GetStatsNumericValue(int level)
        {
            var stat = GetStatsAt(level);
            return stat.Capacities.Values.Sum().ToString();
        }
    }

    [Serializable]
    public class PastureLevelStat
    {
        [SerializeField] private AgeCapacity[] _capacities;
        [SerializeField] private Vector3 _centerPoint;
        [SerializeField] private float _width;

        private Dictionary<DeerAge, int> _capacitiesDictionary;

        public IReadOnlyDictionary<DeerAge, int> Capacities
        {
            get
            {
                if (_capacitiesDictionary == null)
                {
                    _capacitiesDictionary = new();

                    foreach (var item in _capacities)
                        _capacitiesDictionary.Add(item.Age, item.Capacity);
                }

                return _capacitiesDictionary;
            }
        }
        public Vector3 CenterPoint => _centerPoint;
        public float Width => _width;

        [Serializable]
        private class AgeCapacity
        {
            [SerializeField] private DeerAge _deerAge;
            [SerializeField] private int _capacity;

            public DeerAge Age => _deerAge;
            public int Capacity => _capacity;
        }
    }
}