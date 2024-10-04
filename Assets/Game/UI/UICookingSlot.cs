using System;
using System.Linq;
using Game.Gameplay.Crafting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UICookingSlot : MonoBehaviour
    {
        [SerializeField] private Image _mealImage;
        [SerializeField] private TMP_Text _mealCount;
        [SerializeField] private Image _progressBar;
        [SerializeField] private Button _pickButton;

        public CraftingRecepie CurrentRecepie { get; private set; }
        
        public event Action<UICookingSlot> PickRequested;

        public void Init(CraftingRecepie recepie, float timeLeft)
        {
            CurrentRecepie = recepie;

            var mealItem = recepie.OutputItems.Keys.First();
            int mealCount = recepie.OutputItems[mealItem];

            _mealImage.sprite = mealItem.Icon;
            _mealCount.text = mealCount.ToString();

            _mealImage.enabled = true;
            _mealCount.enabled = true;
            Refresh(timeLeft);
        }

        public void Refresh(float timeLeft)
        {
            float progressPart = 1 - timeLeft / CurrentRecepie.CraftTime;
            _progressBar.fillAmount = progressPart;
        }

        public void Clear()
        {
            _mealImage.enabled = false;
            _mealCount.enabled = false;
            CurrentRecepie = null;
        }
    }
}