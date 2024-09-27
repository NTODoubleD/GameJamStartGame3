using System.Collections.Generic;
using Game.Gameplay.Items;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Gameplay.Crafting
{
    [CreateAssetMenu(fileName = "Food Recepie", menuName = "Crafting/Food Recepie")]
    public class FoodRecepie : CraftingRecepie
    {
        [OdinSerialize] private Dictionary<GameItemInfo, int> _fuelItems = new();
        
        public IReadOnlyDictionary<GameItemInfo, int> FuelItems => _fuelItems;
    }
}