using Game.Gameplay.DayCycle;
using Game.UI.Pages;
using System.Linq;
using DoubleDCore.SaveSystem.Base;
using DoubleDCore.UI.Base;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game.Gameplay.Buildings
{
    public abstract class UpgradableBuilding : MonoBehaviour, ISaveObject
    {
        private ConditionResourcesSpender _resourcesSpender;

        [SerializeReference] protected BuildingUpgradesConfig _upgradesConfig;
        [SerializeField] private BuildingViewUpgrader _viewUpgrader;
        [SerializeField] private DayCycleController _dayCycleController;
        [SerializeField] private string _upgradeTitle;

        private int _daysLeftForUpgrade;
        protected IUIManager UIManager;

        public int DaysLeftForUpgrade => _daysLeftForUpgrade;
        public int CurrentLevel { get; private set; } = 1;

        public event UnityAction Upgraded;

        [Inject]
        private void Init(IUIManager uiManager, DiContainer container)
        {
            UIManager = uiManager;

            _resourcesSpender = new ConditionResourcesSpender();
            container.Inject(_resourcesSpender);

            foreach (var upgradeCondition in _upgradesConfig.GetAllUpgradeConditions())
                container.Inject(upgradeCondition);
        }

        private void OnEnable()
        {
            _dayCycleController.DayStarted += OnDayStarted;
        }

        private void OnDisable()
        {
            _dayCycleController.DayStarted -= OnDayStarted;
        }

        private void OnDayStarted()
        {
            if (_daysLeftForUpgrade > 0)
            {
                _daysLeftForUpgrade--;

                if (_daysLeftForUpgrade == 0)
                    DelayedUpgrade();
            }
        }

        public void OpenUpgradePage()
        {
            UIManager.OpenPage<UpgradePage, UpgradeMenuArgument>(GetUpgradeMenuArgument());
        }

        public string Key => "UpgradableBuilding";

        public string GetData()
        {
            return CurrentLevel.ToString();
        }

        public string GetDefaultData()
        {
            return 0.ToString();
        }

        public void OnLoad(string data)
        {
            CurrentLevel = int.Parse(data);
            _viewUpgrader.SetView(CurrentLevel);
        }

        public bool IsMaximalLevel()
        {
            return CurrentLevel == _upgradesConfig.MaximalLevel;
        }

        public bool CanUpgrade()
        {
            if (IsMaximalLevel() || _daysLeftForUpgrade > 0)
                return false;

            var conditions = _upgradesConfig.GetUpgradeConditions(CurrentLevel);
            return conditions.All(condition => condition.IsCompleted());
        }

        public void Upgrade()
        {
            if (CanUpgrade())
            {
                foreach (var condition in _upgradesConfig.GetUpgradeConditions(CurrentLevel))
                    condition.Accept(_resourcesSpender);

                _daysLeftForUpgrade = _upgradesConfig.GetUpgradeDuration(CurrentLevel);
            }
            else
            {
                Debug.LogError("UPGRADE IS NOT POSSIBLE");
            }
        }

        private void DelayedUpgrade()
        {
            CurrentLevel++;
            _viewUpgrader.UpgradeTo(CurrentLevel);
            OnUpgraded();
            Upgraded?.Invoke();
        }

        protected virtual void OnUpgraded()
        {
        }

        private UpgradeMenuArgument GetUpgradeMenuArgument()
        {
            return new UpgradeMenuArgument()
            {
                Label = _upgradeTitle,
                DayDuration = _upgradesConfig.GetUpgradeDuration(CurrentLevel),
                Conditions = _upgradesConfig.GetUpgradeConditions(CurrentLevel).ToList(),
                UpgradableBuilding = this
            };
        }
    }
}