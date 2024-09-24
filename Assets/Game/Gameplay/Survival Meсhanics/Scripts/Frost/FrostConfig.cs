using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    [CreateAssetMenu(fileName = "Frost Config", menuName = "Configs/Frost", order = 0)]
    public class FrostConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<FrostLevel, int> _consumptionLevels = new()
        {
            { FrostLevel.Weak, 0 },
            { FrostLevel.Average, 0 },
            { FrostLevel.Strong, 0 }
        };

        public int GetConsumptionValue(FrostLevel level) => _consumptionLevels[level];
    }
}