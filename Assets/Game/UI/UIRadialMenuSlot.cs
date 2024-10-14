using System;
using Game.Gameplay.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIRadialMenuSlot : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private Button _button;

        private RadialMenuSlotPresenter _presenter;
        public GameItemInfo Item { get; private set; }
        
        public event Action<GameItemInfo> Clicked; 

        public void Initialize(RadialMenuSlotPresenter presenter)
        {
            _presenter = presenter;
            
            Item = _presenter.Item;
            _icon.sprite = _presenter.ItemIcon;
            _count.text = _presenter.ItemCount;

            _presenter.ItemCountChanged += OnCountChanged;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Clicked?.Invoke(Item);
        }
        
        private void OnCountChanged(string newCount)
        {
            _count.text = newCount;
        }

        private void OnDestroy()
        {
            if (_presenter != null)
                _presenter.ItemCountChanged -= OnCountChanged;
        }
    }
}