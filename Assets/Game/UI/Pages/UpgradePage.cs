using System.Collections.Generic;
using DoubleDCore.Extensions;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.Buildings;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Pages
{
    public class UpgradePage : MonoPage, IPayloadPage<UpgradeMenuArgument>
    {
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private TextMeshProUGUI _conditionText;
        [SerializeField] private TextMeshProUGUI _townLevelText;
        [SerializeField] private Button _upgradeButton;

        [Space, SerializeField] private TextMeshProUGUI _upgradeButtonText;
        [SerializeField] private TextMeshProUGUI _remainUpgradeText;

        [Space, SerializeField] private TownHallBuilding _townHallBuilding;

        private ItemStorage _itemStorage;
        private GameInput _inputController;
        private UpgradeMenuArgument _currentArgument;

        [Inject]
        private void Init(ItemStorage itemStorage, GameInput inputController)
        {
            _itemStorage = itemStorage;
            _inputController = inputController;
        }

        public override void Initialize()
        {
            SetCanvasState(false);
        }

        public void Open(UpgradeMenuArgument context)
        {
            _currentArgument = context;

            _inputController.Player.Disable();
            _inputController.UI.Enable();

            SetCanvasState(true);

            _labelText.text = context.Label;

            _conditionText.text = GetText(context);

            _townLevelText.text = $"Текущий уровень - {context.UpgradableBuilding.CurrentLevel}";

            _upgradeButton.interactable = context.UpgradableBuilding.CanUpgrade();

            _upgradeButtonText.text = _currentArgument.UpgradableBuilding.DaysLeftForUpgrade <= 0
                ? "Улучшить"
                : $"До улучшения осталось (дни) - {_currentArgument.UpgradableBuilding.DaysLeftForUpgrade}";

            _remainUpgradeText.text = $"Время улучшения (дни) - {context.DayDuration}";
            _remainUpgradeText.enabled = _currentArgument.UpgradableBuilding.DaysLeftForUpgrade <= 0;
        }

        public override void Close()
        {
            _currentArgument = null;

            SetCanvasState(false);

            _inputController.UI.Disable();
            _inputController.Player.Enable();
        }

        public void Upgrade()
        {
            _currentArgument.UpgradableBuilding.Upgrade();
        }

        private string GetText(UpgradeMenuArgument argument)
        {
            string result = "";
            var conditionVisitor = new ConditionVisitor();

            foreach (var condition in argument.Conditions)
                condition.Accept(conditionVisitor);

            if (conditionVisitor.TownLevel >= 0)
            {
                result += "Уровень юрты - " + $"{conditionVisitor.CurrentTownLevel} / {conditionVisitor.TownLevel}"
                    .Color(conditionVisitor.CurrentTownLevel >= conditionVisitor.TownLevel
                        ? Color.green
                        : Color.red);
                result += "\n";
            }

            foreach (var (item, amount) in conditionVisitor.Items)
            {
                int amountInStorage = _itemStorage.GetCount(item);

                string text = $"{item.Name} - " + $"{amountInStorage} / {amount}\n"
                    .Color(amountInStorage >= amount ? Color.green : Color.red);

                result += text;
            }

            return result;
        }

        private class ConditionVisitor : IUpgradeConditionVisitor
        {
            public int TownLevel { get; private set; } = -1;
            public int CurrentTownLevel { get; private set; } = -1;

            public IReadOnlyDictionary<ItemInfo, int> Items { get; private set; }

            public void Visit(TownHallUpgradeCondition condition)
            {
                TownLevel = condition.NecessaryLevel;
                CurrentTownLevel = TownHallLocator.Instance.TownHall.CurrentLevel;
            }

            public void Visit(ResourcesUpgradeCondition condition)
            {
                Items = condition.NeccessaryItems;
            }
        }
    }

    public class UpgradeMenuArgument
    {
        public string Label;
        public int DayDuration;
        public List<IUpgradeCondition> Conditions;
        public UpgradableBuilding UpgradableBuilding;
    }
}