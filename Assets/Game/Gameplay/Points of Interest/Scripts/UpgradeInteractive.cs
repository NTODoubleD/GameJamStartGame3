using System.Collections.Generic;
using Game.Gameplay.Interaction;
using Game.Gameplay.SurvivalMeсhanics.Fatigue;
using Game.UI.Pages;
using Zenject;

public class UpgradeInteractive : UpgradingInteractiveObject
{
    private FatigueRadialButtonsHelper _helper;
    
    [Inject]
    private void Init(FatigueRadialButtonsHelper helper)
    {
        _helper = helper;
    }
    
    public override void Interact()
    {
        var argument = GetRadialMenuArgument();

        if (_helper.IsFatigueEffectActive())
            argument.Buttons = new List<RadialButtonInfo>() 
                { _helper.GetTiredButtonInfo() };
        
        UIManager.OpenPage<RadialMenuPage, RadialMenuArgument>(argument);
    }
}