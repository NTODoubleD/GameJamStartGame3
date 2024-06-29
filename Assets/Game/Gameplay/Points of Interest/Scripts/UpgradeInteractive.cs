using Game.Gameplay.Interaction;
using Game.UI.Pages;

public class UpgradeInteractive : UpgradingInteractiveObject
{
    public override void Interact()
    {
        UIManager.OpenPage<RadialMenuPage, RadialMenuArgument>(GetRadialMenuArgument());
    }
}