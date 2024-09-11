using System.Collections.Generic;
using DoubleDCore.Extensions;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Infrastructure.Items;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class ResourceWatcherPage : MonoPage, IPayloadPage<ResourcePageArgument>
    {
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private TextMeshProUGUI _text;

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
            SetCanvasState(true);

            _inputController.Player.Disable();
            _inputController.UI.Enable();

            _labelText.text = context.Label;

            _text.text = "";

            if (string.IsNullOrEmpty(context.TextHeading) == false)
                _text.text += context.TextHeading + "\n";

            foreach (var (itemInfo, amount) in context.Resource)
            {
                var amountText = amount.ToString();
                amountText = amountText.Color(amount > 0 ? Color.green : Color.red);

                _text.text += $"{itemInfo.Name} - " + amountText;
                _text.text += "\n";
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
        public string TextHeading = "";
        public Dictionary<ItemInfo, int> Resource;
    }
}