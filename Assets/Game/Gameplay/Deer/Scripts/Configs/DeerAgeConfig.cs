using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "Deer Age Config", menuName = "Configs/DeerAge")]
    public class DeerAgeConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<DeerAge, int> _ageTable = new()
        {
            { DeerAge.Adult, 2 },
            { DeerAge.Old, 5 },
            { DeerAge.None, 7 }
        };

        private Dictionary<int, DeerAge> _reversedAgeTable;
        
        public IReadOnlyDictionary<DeerAge, int> AgeTable => _ageTable;

        public IReadOnlyDictionary<int, DeerAge> ReversedAgeTable
        {
            get
            {
                if (_reversedAgeTable != null)
                    return _reversedAgeTable;
                
                _reversedAgeTable = new Dictionary<int, DeerAge>();
                
                foreach (var pair in _ageTable)
                    _reversedAgeTable.Add(pair.Value, pair.Key);

                return _reversedAgeTable;
            }
        }
    }
}