using System.Collections.Generic;
using DoubleDCore.Extensions;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.Buildings;
using Game.Gameplay.Items;
using Game.Infrastructure.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Pages
{
    public class UpgradePage : MonoPage, IPayloadPage<UpgradeMenuArgument>
    {
        [SerializeField] private UIUpgradeCondition _conditionPrefab;
        [SerializeField] private Transform _conditionsRoot;
        [SerializeField] private TextMeshProUGUI _upgradeDescription;
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private TextMeshProUGUI _townLevelText;
        [SerializeField] private Button _upgradeButton;

        [Space, SerializeField] private TextMeshProUGUI _upgradeButtonText;
        [SerializeField] private TextMeshProUGUI _remainUpgradeText;

        [Space, SerializeField] private TownHallBuilding _townHallBuilding;

        private readonly List<UIUpgradeCondition> _currentConditions = new();
        
        private ItemStorage _itemStorage;
        private GameInput _inputController;
        private UpgradeMenuArgument _currentArgument;
        private BuildingsLocator _buildingsLocator;

        [Inject]
        private void Init(ItemStorage itemStorage, GameInput inputController, BuildingsLocator buildingsLocator)
        {
            _itemStorage = itemStorage;
            _inputController = inputController;
            _buildingsLocator = buildingsLocator;
        }

        public override void Initialize()
        {
            SetCanvasState(false);
        }

        private readonly TranslatedText _thisLevelText =
            new("Текущий уровень - {0}", "The current level is {0}");

        private readonly TranslatedText _upgradeText =
            new("Улучшить", "Improve");

        private readonly TranslatedText _stillImprovementText =
            new("До улучшения осталось (дни) - {0}", "There are still {0} to improve");

        private readonly TranslatedText _upgradeTimeText =
            new("Время улучшения (дни) - {0}", "Improvement time - {0}");

        public void Open(UpgradeMenuArgument context)
        {
            _currentArgument = context;

            _inputController.Player.Disable();
            _inputController.UI.Enable();

            SetCanvasState(true);

            _labelText.text = context.Label;

            FillConditions(context);

            _upgradeDescription.text = GetUpgradeText(context);

            _townLevelText.text = string.Format(_thisLevelText.GetText(), context.UpgradableBuilding.CurrentLevel);

            _upgradeButton.interactable = context.UpgradableBuilding.CanUpgrade();

            _upgradeButtonText.text = _currentArgument.UpgradableBuilding.DaysLeftForUpgrade <= 0
                ? _upgradeText.GetText()
                : string.Format(_stillImprovementText.GetText(),
                    _currentArgument.UpgradableBuilding.DaysLeftForUpgrade);

            _remainUpgradeText.text = string.Format(_upgradeTimeText.GetText(), context.DayDuration);
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

        private readonly TranslatedText _urtLevel = new("Уровень юрты - ", "Yurt level - ");

        private void FillConditions(UpgradeMenuArgument argument)
        {
            for (int i = 0; i < _currentConditions.Count; i++)
                Destroy(_currentConditions[i].gameObject);
            
            _currentConditions.Clear();
            
            var conditionVisitor = new ConditionVisitor(_buildingsLocator);

            foreach (var condition in argument.Conditions)
                condition.Accept(conditionVisitor);

            if (conditionVisitor.TownLevel >= 0)
            {
                var conditionObject = Instantiate(_conditionPrefab, _conditionsRoot);

                string urtConditionDescription = 
                    _urtLevel.GetText() + $"{conditionVisitor.CurrentTownLevel}/{conditionVisitor.TownLevel}"
                        .Color(conditionVisitor.CurrentTownLevel >= conditionVisitor.TownLevel 
                            ? Color.green 
                            : Color.red);
                
                conditionObject.Initialize(urtConditionDescription);
                _currentConditions.Add(conditionObject);
            }
            
            foreach (var (item, amount) in conditionVisitor.Items)
            {
                var conditionObject = Instantiate(_conditionPrefab, _conditionsRoot);
                conditionObject.Initialize(item, _itemStorage.GetCount(item), amount);
                _currentConditions.Add(conditionObject);
            }
        }

        private string GetUpgradeText(UpgradeMenuArgument argument)
        {
            var levelsConfig = argument.UpgradableBuilding.GetLevelsConfig();
            int currentLevel = argument.UpgradableBuilding.CurrentLevel;
            string result = $"{levelsConfig.Description}";

            if (levelsConfig.CanShowStatsNumericValue())
            {
                result +=
                    $": <color=#{ColorUtility.ToHtmlStringRGB(Color.green)}>{levelsConfig.GetStatsNumericValue(currentLevel)}</color>" +
                    $" -> <color=#{ColorUtility.ToHtmlStringRGB(Color.green)}>{levelsConfig.GetStatsNumericValue(currentLevel + 1)}</color>";
            }
            
            return result;
        }

        private class ConditionVisitor : IUpgradeConditionVisitor
        {
            private TownHallBuilding _townHallBuilding;
            
            public int TownLevel { get; private set; } = -1;
            public int CurrentTownLevel { get; private set; } = -1;
            public IReadOnlyDictionary<GameItemInfo, int> Items { get; private set; }

            public ConditionVisitor(BuildingsLocator buildingsLocator)
            {
                _townHallBuilding = buildingsLocator.TownHall;
            }

            public void Visit(TownHallUpgradeCondition condition)
            {
                TownLevel = condition.NecessaryLevel;
                CurrentTownLevel = _townHallBuilding.CurrentLevel;
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