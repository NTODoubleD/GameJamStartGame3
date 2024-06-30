using Game.UI.Pages;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class UpgradeLevelCondition : ConditionObject
    {
        [SerializeField] private UpgradableBuilding _upgradableBuilding;

        public override bool ConditionIsDone()
        {
            return _upgradableBuilding.IsMaximalLevel() == false;
        }
    }
}