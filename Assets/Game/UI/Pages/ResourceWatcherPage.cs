using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.Extensions;
using DoubleDTeam.InputSystem;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using Game.Infrastructure.Items;
using Game.InputMaps;
using TMPro;
using UnityEngine;

namespace Game.UI.Pages
{
    public class ResourceWatcherPage : MonoPage, IPayloadPage<ResourcePageArgument>
    {
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private TextMeshProUGUI _text;

        private InputController _inputController;

        private void Awake()
        {
            _inputController = Services.ProjectContext.GetModule<InputController>();

            Close();
        }

        public void Open(ResourcePageArgument context)
        {
            SetCanvasState(true);

            _inputController.EnableMap<UIInputMap>();

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

            _inputController.EnableMap<PlayerInputMap>();
        }
    }

    public class ResourcePageArgument
    {
        public string Label;
        public string TextHeading = "";
        public Dictionary<ItemInfo, int> Resource;
    }
}