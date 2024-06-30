using DoubleDTeam.SaveSystem.Base;
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

        public int CurrentLevel { get; private set; } = 1;

        public event UnityAction Upgraded;

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
            if (IsMaximalLevel())
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

                CurrentLevel++;
                _viewUpgrader.UpgradeTo(CurrentLevel);
                Upgraded?.Invoke();
            }
            else
            {
                Debug.LogError("UPGRADE IS NOT POSSIBLE");
            }
        }
    }
}