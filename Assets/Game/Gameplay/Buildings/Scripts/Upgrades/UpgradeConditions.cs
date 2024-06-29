using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public interface IUpgradeCondition
    {
        bool IsCompleted();
    }

    [Serializable]
    public class TownHallUpgradeCondition : IUpgradeCondition
    {
        [SerializeField] private UpgradableBuilding _townHall;
        [SerializeField] private int _neccessaryLevel;

        bool IUpgradeCondition.IsCompleted()
        {
            return _townHall.CurrentLevel >= _neccessaryLevel;
        }

        public void Test()
        {

        }
    }

    [Serializable]
    public class ResourcesUpgradeCondition : IUpgradeCondition
    {
        bool IUpgradeCondition.IsCompleted()
        {
            return true;
        }
    }
}