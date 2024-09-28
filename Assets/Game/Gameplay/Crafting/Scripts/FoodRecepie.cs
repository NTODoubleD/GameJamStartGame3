using System;
using System.Collections.Generic;
using Game.Gameplay.Items;
using UnityEngine;

namespace Game.Gameplay.Crafting
{
    [CreateAssetMenu(fileName = "Food Recepie", menuName = "Crafting/Food Recepie")]
    public class FoodRecepie : ScriptableObject, ICraftingRecepie
    {
        [SerializeField] private FoodRecepieItem _outputItem;
        [SerializeField] private FoodRecepieItem _inputItem;
        [SerializeField] private FoodRecepieItem _fuelItem;
        
        public IReadOnlyDictionary<GameItemInfo, int> OutputItems => new Dictionary<GameItemInfo, int> { { _outputItem.Item, _outputItem.Count } };
        public IReadOnlyDictionary<GameItemInfo, int> InputItems => new Dictionary<GameItemInfo, int> { { _inputItem.Item, _inputItem.Count } };
        
        public FoodRecepieItem OutputItem => _outputItem;
        public FoodRecepieItem InputItem => _inputItem;
        public FoodRecepieItem FuelItem => _fuelItem;
    }
    
    [Serializable]
    public struct FoodRecepieItem
    {
        public GameItemInfo Item;
        public int Count;
    }
}