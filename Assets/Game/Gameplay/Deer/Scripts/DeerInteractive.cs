using System.Collections.Generic;
using DoubleDCore.TranslationTools.Data;
using Game.Gameplay.Interaction;
using Game.Gameplay.SurvivalMeсhanics.Fatigue;
using Game.UI.Pages;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game.Gameplay
{
    public class DeerInteractive : MultipleInteractiveObject
    {
        [SerializeField] private Deer _deer;

        private FatigueRadialButtonsHelper _helper;
        
        public UnityEvent Interacted;
    
        [Inject]
        private void Init(FatigueRadialButtonsHelper helper)
        {
            _helper = helper;
        }

        protected override RadialMenuArgument GetRadialMenuArgument()
        {
            return new RadialMenuArgument()
            {
                Name = StaticLanguageProvider.GetLanguage() == LanguageType.Ru
                    ? _name + "\n" + _deer.DeerInfo.Name
                    : _enName + "\n" + _deer.DeerInfo.Name,
                Buttons = _operations
            };
        }

        public override void Interact()
        {
            var argument = GetRadialMenuArgument();

            if (_helper.IsFatigueEffectActive())
                argument.Buttons = new List<RadialButtonInfo>() 
                    { _helper.GetTiredButtonInfo() };
        
            UIManager.OpenPage<RadialMenuPage, RadialMenuArgument>(argument);
            Interacted?.Invoke();
        }
    }
}