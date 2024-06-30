using DoubleDTeam.SaveSystem.Base;
using Game.Gameplay.DayCycle;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.Buildings
{
    public abstract class UpgradableBuilding : MonoBehaviour, ISaveObject
    {
        private readonly ConditionResourcesSpender _resourcesSpender = new();

        [SerializeReference] protected BuildingUpgradesConfig _upgradesConfig;
        [SerializeField] private BuildingViewUpgrader _viewUpgrader;
        [SerializeField] private DayCycleController _dayCycleController;

        private int _daysLeftForUpgrade;

        public int CurrentLevel { get; private set; } = 1;

        public event UnityAction Upgraded;

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

        public string GetData()
        {
            return CurrentLevel.ToString();
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
            Upgraded?.Invoke();
        }
    }
}