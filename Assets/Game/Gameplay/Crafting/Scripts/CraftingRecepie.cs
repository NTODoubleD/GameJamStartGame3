using System.Collections.Generic;
using Game.Gameplay.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Gameplay.Crafting
{
    [CreateAssetMenu(fileName = "Crafting Recepie", menuName = "Crafting/Recepie")]
    public class CraftingRecepie : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<GameItemInfo, int> _outputItems = new();
        [OdinSerialize] private Dictionary<GameItemInfo, int> _inputItems = new();
        
        public IReadOnlyDictionary<GameItemInfo, int> OutputItems => _outputItems;
        public IReadOnlyDictionary<GameItemInfo, int> InputItems => _inputItems;
    }
}