using System;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class UICraftingArrow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _craftingTime;
        [SerializeField] private GameObject _canCookVisual;
        [SerializeField] private GameObject _canNotCookVisual;
        
        private TimeSpan _cachedCraftingTime;
        
        public void Initialize(float craftingTime)
        {
            _cachedCraftingTime = TimeSpan.FromSeconds(craftingTime);
            _craftingTime.text = $"{_cachedCraftingTime.Minutes:D1}:{_cachedCraftingTime.Seconds:D2}";
        }

        public void SetVisualActive(bool value)
        {
            _canCookVisual.SetActive(value);
            _canNotCookVisual.SetActive(!value);
        }
    }
}