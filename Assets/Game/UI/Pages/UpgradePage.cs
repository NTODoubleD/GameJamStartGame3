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
        [SerializeField] private TextMeshProUGUI _townLevelText;
        [SerializeField] private Button _upgradeButton;

        [Space, SerializeField] private TextMeshProUGUI _upgradeButtonText;
        [SerializeField] private TextMeshProUGUI _remainUpgradeText;

        [Space, SerializeField] private TownHallBuilding _townHallBuilding;

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

            _conditionText.text = GetText(context);

            _townLevelText.text = $"Уровень юрты - {_townHallBuilding.CurrentLevel}";

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

            Services.ProjectContext.GetModule<InputController>().EnableMap<PlayerInputMap>();
        }

        public void Upgrade()
        {
            _currentArgument.UpgradableBuilding.Upgrade();
        }

        private string GetText(UpgradeMenuArgument argument)
        {
            string result = "";

            foreach (var condition in argument.Conditions)
            {
                var conditionVisitor = new ConditionVisitor();

                condition.Accept(conditionVisitor);

                if (conditionVisitor.TownLevel >= 0)
                {
                    result += "Уровень юрты - " + conditionVisitor.TownLevel.ToString()
                        .Color(conditionVisitor.TownLevel >= conditionVisitor.CurrentTownLevel
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
                CurrentTownLevel = condition.CurrentLevel;
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