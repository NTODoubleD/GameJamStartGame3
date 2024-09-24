using System;
using System.Collections.Generic;
using System.Linq;
using Game.Infrastructure.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Gameplay.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Deer Cut Config", menuName = "Configs/DeerCut")]
    public class DeerCutConfig : SerializedScriptableObject
    {
        [Header("Other Loot")] 
        [OdinSerialize] private Dictionary<ItemInfo, int> _lootAmount = new();
        
        [Header("Meat")]
        [OdinSerialize] private ItemInfo _meatInfo;
        [OdinSerialize] private Dictionary<float, int> _meatAmountByHunger = new()
        {
            {0.4f, 4},
            {0.6f, 6},
            {0.8f, 8},
            {1f, 10}
        };

        public Dictionary<ItemInfo, int> GetLoot(float hungerDegree)
        {
            var loot = new Dictionary<ItemInfo, int>(_lootAmount)
            {
                [_meatInfo] = GetMeatAmount(hungerDegree)
            };
            
            Debug.Log($"HUNGER: {hungerDegree}, MEAT: {loot[_meatInfo]}");
            
            return loot;
        }
        
        private int GetMeatAmount(float hunger)
        {
            foreach (var key in _meatAmountByHunger.Keys.OrderBy(x => x))
                if (hunger <= key)
                    return _meatAmountByHunger[key];;

            throw new ArgumentOutOfRangeException($"HUNGER DEGREE {hunger} IS OUT OF RANGE");
        }
    }
}