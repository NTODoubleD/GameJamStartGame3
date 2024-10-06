using DoubleDCore.Service;
using Game.UI.Pages;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.SurvivalMeсhanics.Fatigue
{
    public class FatigueRadialButtonsHelper : MonoService
    {
        [SerializeField] private RadialButtonInfo _fatigueButton;
        
        private FatigueController _fatigueController;
        
        [Inject]
        private void Init(FatigueController fatigueController)
        {
            _fatigueController = fatigueController;
        }
        
        public bool IsFatigueEffectActive()
        {
            return _fatigueController.IsEffectActive;
        }

        public RadialButtonInfo GetTiredButtonInfo()
        {
            return _fatigueButton;
        }
    }
}