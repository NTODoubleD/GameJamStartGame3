using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.Extensions;
using DoubleDTeam.InputSystem;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using Game.Gameplay.Buildings;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using Game.InputMaps;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Pages
{
    public class UpgradePage : MonoPage, IPayloadPage<UpgradeMenuArgument>
    {
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private TextMeshProUGUI _conditionText;
        [SerializeField] private Button _upgradeButton;

        private ItemStorage _itemStorage;
        private UpgradeMenuArgument _currentArgument;

        private void Awake()
        {
            _itemStorage = Services.ProjectContext.GetModule<ItemStorage>();

            Close();
        }

        public void Open(UpgradeMenuArgument context)
        {
            _currentArgument = context;

            SetCanvasState(true);

            _labelText.text = context.Label;

            string conditionText = "";

            foreach (var (item, amount) in context.ItemsDictionary)
            {
                int amountInStorage = _itemStorage.GetCount(item);

                string text = $"{item.Name} - " + $"{amount}/{amountInStorage}\n"
                    .Color(amountInStorage >= amount ? Color.green : Color.red);

                conditionText += text;
            }

            _conditionText.text = conditionText;

            _upgradeButton.interactable = context.UpgradableBuilding.CanUpgrade();
        }

        public override void Close()
        {
            _currentArgument = null;

            SetCanvasState(false);

            Services.ProjectContext.GetModule<InputController>().EnableMap<PlayerInputMap>();
        }

        public void Upgrade()
        {
            _currentArgument.UpgradableBuilding.Upgrade();
        }
    }

    public class UpgradeMenuArgument
    {
        public string Label;
        public Dictionary<ItemInfo, int> ItemsDictionary;
        public UpgradableBuilding UpgradableBuilding;
    }
}