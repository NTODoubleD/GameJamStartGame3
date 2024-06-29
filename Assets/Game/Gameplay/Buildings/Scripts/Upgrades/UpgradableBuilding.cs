using DoubleDTeam.SaveSystem.Base;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public abstract class UpgradableBuilding : MonoBehaviour, ISaveObject
    {
        private readonly ConditionResourcesSpender _resourcesSpender = new();

        [SerializeReference] private BuildingUpgradesConfig _upgradesConfig;
        [SerializeField] private BuildingViewUpgrader _viewUpgrader;

        public int CurrentLevel { get; private set; } = 1;

        public string GetData()
        {
            return CurrentLevel.ToString();
        }

        public void OnLoad(string data)
        {
            CurrentLevel = int.Parse(data);
            _viewUpgrader.SetView(CurrentLevel);
            OnLoaded();
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
                OnUpgraded();
            }
            else
            {
                Debug.LogError("UPGRADE IS NOT POSSIBLE");
            }
        }

        protected abstract void OnUpgraded();
        protected abstract void OnLoaded();
    }
}