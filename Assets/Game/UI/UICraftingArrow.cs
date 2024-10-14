using System;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class UICraftingArrow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _craftingTime;
        
        private TimeSpan _cachedCraftingTime;
        
        public void Initialize(float craftingTime)
        {
            _cachedCraftingTime = TimeSpan.FromSeconds(craftingTime);
            _craftingTime.text = $"{_cachedCraftingTime.Minutes:D1}:{_cachedCraftingTime.Seconds:D2}";
        }
    }
}