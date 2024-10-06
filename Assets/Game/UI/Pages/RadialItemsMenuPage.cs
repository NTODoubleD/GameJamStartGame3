using System;
using DG.Tweening;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.Items;
using Game.Infrastructure.Storage;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class RadialItemsMenuPage : MonoPage, IUIPage
    {
        [SerializeField] private RadialSlot[] _radialSlots;
        [SerializeField] private Transform _slotsRoot;

        [Header("Animation")] [SerializeField] private float _transitDuration;

        private ItemStorage _itemStorage;
        private Tweener _currentTweener;

        [Inject]
        private void Init(ItemStorage itemStorage)
        {
            _itemStorage = itemStorage;
        }

        public override void Initialize()
        {
            foreach (var radialSlot in _radialSlots)
                radialSlot.View.Initialize(new RadialMenuSlotPresenter(radialSlot.Item, _itemStorage));
        }

        public void Open()
        {
            SetCanvasState(true);

            if (_currentTweener != null && _currentTweener.IsActive())
                _currentTweener.Kill();

            _currentTweener = _slotsRoot.DOScale(1, _transitDuration).SetEase(Ease.OutBack);
        }

        public override void Close()
        {
            if (_currentTweener != null && _currentTweener.IsActive())
                _currentTweener.Kill();

            _currentTweener = _slotsRoot.DOScale(0, _transitDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() => SetCanvasState(false));
        }

        [Serializable]
        private struct RadialSlot
        {
            public UIRadialMenuSlot View;
            public GameItemInfo Item;
        }
    }
}