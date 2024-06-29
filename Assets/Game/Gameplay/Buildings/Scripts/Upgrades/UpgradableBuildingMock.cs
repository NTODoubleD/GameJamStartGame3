using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class UpgradableBuildingMock : UpgradableBuilding
    {
        protected override void OnUpgraded()
        {
            Debug.Log($"New Level Is {CurrentLevel}");
        }
    }
}