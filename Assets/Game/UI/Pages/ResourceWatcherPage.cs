using System.Collections.Generic;
using DoubleDCore.Extensions;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.Items;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class ResourceWatcherPage : MonoPage, IPayloadPage<ResourcePageArgument>
    {
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private TextMeshProUGUI _headingText;
        [SerializeField] private UIUpgradeCondition _resourceViewPrefab;
        [SerializeField] private Transform _resourcesRoot;

        private readonly List<UIUpgradeCondition> _currentResourceViews = new();
        
        private GameInput _inputController;

        [Inject]
        private void Init(GameInput inputController)
        {
            _inputController = inputController;
        }

        public override void Initialize()
        {
            SetCanvasState(false);
        }

        public void Open(ResourcePageArgument context)
        {
            for (int i = 0; i < _currentResourceViews.Count; i++)
                Destroy(_currentResourceViews[i].gameObject);
            
            _currentResourceViews.Clear();
            
            SetCanvasState(true);

            _inputController.Player.Disable();
            _inputController.UI.Enable();

            _labelText.text = context.Label;
            _headingText.text = context.TextHeading;

            foreach (var (itemInfo, amount) in context.Resource)
            {
                var amountText = $" + {amount}";
                amountText = amountText.Color(amount > 0 ? Color.green : Color.red);

                var resourceView = Instantiate(_resourceViewPrefab, _resourcesRoot);
                resourceView.Initialize(itemInfo, amountText);
                _currentResourceViews.Add(resourceView);
            }
        }

        public override void Close()
        {
            SetCanvasState(false);

            _inputController.UI.Disable();
            _inputController.Player.Enable();
        }
    }

    public class ResourcePageArgument
    {
        public string Label;
        public string TextHeading;
        public Dictionary<GameItemInfo, int> Resource;
    }
}