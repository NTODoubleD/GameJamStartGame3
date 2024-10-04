using System;
using Game.Gameplay.Crafting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UICraftingRecepie : MonoBehaviour
    {
        [SerializeField] private Button _clickButton;
        
        public CraftingRecepie RecepieData { get; private set; }

        public event Action<CraftingRecepie> Clicked;

        private void OnEnable()
        {
            _clickButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _clickButton.onClick.RemoveListener(OnButtonClicked);
        }

        public void Init(CraftingRecepie recepie)
        {
            RecepieData = recepie;
        }

        public void SetAvailable(bool isAvailable)
        {
            _clickButton.interactable = isAvailable;
        }

        private void OnButtonClicked()
        {
            Clicked?.Invoke(RecepieData);
        }
    }
}