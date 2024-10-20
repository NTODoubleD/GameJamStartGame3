using System.Collections.Generic;
using DG.Tweening;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.Items;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class ResourcePage : MonoPage, IUIPage
    {
        [SerializeField] private UIResource _prefab;
        [SerializeField] private RectTransform _container;
        [SerializeField] private CanvasGroup _canvasGroup;

        private ItemStorage _itemStorage;

        private readonly Dictionary<string, UIResource> _uiResources = new();

        [Inject]
        private void Init(ItemStorage itemStorage)
        {
            _itemStorage = itemStorage;
        }

        public override void Initialize()
        {
            foreach (var itemInfo in _itemStorage.Resources.Keys)
            {
                var inst = Instantiate(_prefab, Vector3.zero, Quaternion.identity, _container);

                if (itemInfo is GameItemInfo gameItemInfo == false)
                    continue;

                _uiResources.Add(gameItemInfo.ID, inst);
            }

            Open();
        }

        public void Open()
        {
            if (PageIsDisplayed)
                return;

            _itemStorage.ItemAdded += OnItemAdded;
            _itemStorage.ItemRemoved += OnItemRemoved;

            foreach (var (itemInfo, count) in _itemStorage.Resources)
                RefreshUIResource(itemInfo, count, 0);

            SetCanvasState(true);
            _canvasGroup.DOFade(1, 0.4f);
        }

        public override void Close()
        {
            if (PageIsDisplayed == false)
                return;

            _canvasGroup.DOFade(0, 0.4f).OnComplete(() => SetCanvasState(false));

            _itemStorage.ItemAdded -= OnItemAdded;
            _itemStorage.ItemRemoved -= OnItemRemoved;
        }

        private void OnItemAdded(ItemInfo itemInfo, int delta)
        {
            OnItemChanged(itemInfo, delta);
        }

        private void OnItemRemoved(ItemInfo itemInfo, int delta)
        {
            OnItemChanged(itemInfo, -delta);
        }

        private void OnItemChanged(ItemInfo itemInfo, int newValue)
        {
            RefreshUIResource(itemInfo, _itemStorage.GetCount(itemInfo), newValue);
        }

        private void RefreshUIResource(ItemInfo itemInfo, int count, int delta)
        {
            if (itemInfo is GameItemInfo gameItemInfo == false)
                return;

            if (_uiResources.ContainsKey(gameItemInfo.ID) == false)
                return;

            var uiResource = _uiResources[gameItemInfo.ID];

            uiResource.Initialize(gameItemInfo);
            uiResource.Refresh(count);
            uiResource.PlayFeedback(delta);
        }
    }
}