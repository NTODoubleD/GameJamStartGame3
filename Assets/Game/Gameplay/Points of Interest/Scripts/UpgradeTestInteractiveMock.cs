using Game.Gameplay.Buildings;
using Game.Gameplay.Interaction;
using UnityEngine;

public class UpgradeTestInteractiveMock : UpgradingInteractiveObject
{
    [SerializeField] private TownHallBuilding _townhall;

    public override void Interact()
    {
        if (_townhall.CanUpgrade())
        {
            _townhall.Upgrade();
            Debug.Log($"Upgraded");
        }
        else
        {
            Debug.Log("Not Upgraded");
        }
    }
}