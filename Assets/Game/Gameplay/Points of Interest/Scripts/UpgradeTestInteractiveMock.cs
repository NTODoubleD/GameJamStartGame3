using Game.Gameplay.Buildings;
using Game.Gameplay.Interaction;
using UnityEngine;

public class UpgradeTestInteractiveMock : InteractiveObject
{
    [SerializeField] private TownHallBuilding _townhall;

    public override void Interact()
    {
        _townhall.Upgrade();
    }
}