using System;
using System.Collections.Generic;
using DoubleDCore.TranslationTools;
using Game.Gameplay.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay.Crafting
{
    [CreateAssetMenu(menuName = "Configs/Cooking", fileName = "Cooking Config")]
    public class CookingConfig : SerializedScriptableObject
    {
        [SerializeField] private CraftingRecepie[] _recepies;
        [SerializeField] private GameItemInfo _fuelItem;
        [SerializeField] private float _fuelTimeAddition;
        [SerializeField] private int _cookingPlaceCount;
        
        public IReadOnlyCollection<CraftingRecepie> Recepies => _recepies;
        public int CookingPlaceCount => _cookingPlaceCount;
        
        public (GameItemInfo, float) GetFuelInfo() => (_fuelItem, _fuelTimeAddition);
        
    }

    [Serializable]
    public class CookingGroup
    {
        [SerializeField] private TranslatedText _name;
        [SerializeField] private CraftingRecepie _recepie;
        
        public TranslatedText Name => _name;
        public CraftingRecepie Recepie => _recepie;
    }
}