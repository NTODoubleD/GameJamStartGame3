using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    [CreateAssetMenu(fileName = "Frost Config", menuName = "Configs/Frost", order = 0)]
    public class FrostConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<FrostLevel, float> _consumptionLevels = new()
        {
            { FrostLevel.Weak, 0 },
            { FrostLevel.Average, 0 },
            { FrostLevel.Strong, 0 }
        };
        
        [Header("Frost Start Settings")]
        [OdinSerialize] private Dictionary<FrostLevel, FrostSettings> _frostStartSettings = new()
        {
            { FrostLevel.Average, new FrostSettings(1, 180, new []{ 60, 90, 120 }) },
            { FrostLevel.Strong, new FrostSettings(5, 120, new []{ 60 }) }
        };

        public float GetConsumptionValue(FrostLevel level) => _consumptionLevels[level];
        
        public FrostSettings GetFrostSettings(FrostLevel level) => _frostStartSettings[level];

        [Serializable]
        public struct FrostSettings
        {
            public int DayPeriod;
            public int Duration;
            public int[] StartDelays;
            
            public FrostSettings(int dayPeriod, int duration, int[] startDelays)
            {
                Duration = duration;
                StartDelays = startDelays;
                DayPeriod = dayPeriod;
            }
        }
    }
}