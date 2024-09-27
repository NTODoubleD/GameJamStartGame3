using System;
using System.Collections.Generic;
using DoubleDCore.TranslationTools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay.Crafting
{
    [CreateAssetMenu(menuName = "Cooking Config", fileName = "Configs/Cooking")]
    public class CookingConfig : SerializedScriptableObject
    {
        [SerializeField] private CookingGroup[] _cookingGroups;
        
        public IReadOnlyCollection<CookingGroup> CookingGroups => _cookingGroups;
    }

    [Serializable]
    public class CookingGroup
    {
        [SerializeField] private TranslatedText _name;
        [SerializeField] private FoodRecepie _recepie;
        [SerializeField] private float _cookTime;
        
        public TranslatedText Name => _name;
        public FoodRecepie Recepie => _recepie;
        public float CookTime => _cookTime;
    }
}