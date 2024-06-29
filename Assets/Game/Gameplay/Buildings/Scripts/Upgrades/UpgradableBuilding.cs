using DoubleDTeam.SaveSystem.Base;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public abstract class UpgradableBuilding : MonoBehaviour, ISaveObject
    {
        [SerializeReference] private List<IUpgradeCondition> _conditions;

        public int CurrentLevel { get; private set; }

        public string GetData()
        {
            throw new System.NotImplementedException();
        }

        public void OnLoad(string data)
        {
            throw new System.NotImplementedException();
        }

        #region EDITOR_METHODS

        public void AddTownHallCondition()
        {
            AddCondition(new TownHallUpgradeCondition());
        }

        public void AddResourcesCondition()
        {
            AddCondition(new ResourcesUpgradeCondition());
        }

        private void AddCondition(IUpgradeCondition condition)
        {
            _conditions.Add(condition);
        }

        #endregion
    }
}