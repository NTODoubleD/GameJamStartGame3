using System.Collections.Generic;
using Game.Gameplay.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Hunger
{
    [CreateAssetMenu(menuName = "Configs/Eating", fileName = "Eating Config")]
    public class EatingConfig : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<GameItemInfo, float> _food = new();

        public IReadOnlyCollection<GameItemInfo> Food => _food.Keys;

        public float GetRestoreValue(GameItemInfo foodItem)
        {
            return _food[foodItem];
        }
    }
}