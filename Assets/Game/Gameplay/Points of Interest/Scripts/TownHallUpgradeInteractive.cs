using System.Collections.Generic;
using Game.Gameplay.Interaction;
using Game.Gameplay.SurvivalMeсhanics.Fatigue;
using Game.UI.Pages;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class TownHallUpgradeInteractive : UpgradingInteractiveObject
{
    [PropertySpace] 
    [SerializeField] private int _sleepActionIndex;
    
    private FatigueRadialButtonsHelper _fatigueRadialButtonsHelper;
    
    [Inject]
    private void Init(FatigueRadialButtonsHelper helper)
    {
        _fatigueRadialButtonsHelper = helper;
    }
    
    public override void Interact()
    {
        var radialMenuArgument = GetRadialMenuArgument();

        if (_fatigueRadialButtonsHelper.IsFatigueEffectActive())
        {
            var sleepAction = radialMenuArgument.Buttons[_sleepActionIndex];
            radialMenuArgument.Buttons = new List<RadialButtonInfo>() { sleepAction };
        }
        
        UIManager.OpenPage<RadialMenuPage, RadialMenuArgument>(radialMenuArgument);
    }
}