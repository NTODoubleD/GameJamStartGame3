using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Endurance
{
    [CreateAssetMenu(fileName = "Endurance Config", menuName = "Configs/Endurance", order = 0)]
    public class EnduranceConfig : SerializedScriptableObject
    {
        [OdinSerialize] 
        private Dictionary<ActionType, float> _consumptions = new()
        {
            { ActionType.Constant, 0.1f },
            { ActionType.Sprint, 0.2f }
        };
        
        [SerializeField] private float _restoreDayValue = 100;

        public float RestoreDayValue => _restoreDayValue;
        
        public float GetConsumption(ActionType actionType) => _consumptions[actionType];
    }
}