using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public interface IUpgradeCondition
    {
        bool IsCompleted();
        void Accept(IUpgradeConditionVisitor visitor);
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

        void IUpgradeCondition.Accept(IUpgradeConditionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [Serializable]
    public class ResourcesUpgradeCondition : IUpgradeCondition
    {
        void IUpgradeCondition.Accept(IUpgradeConditionVisitor visitor)
        {
            visitor.Visit(this);
        }

        bool IUpgradeCondition.IsCompleted()
        {
            return true;
        }
    }
}