using System.Collections.Generic;
using Game.Gameplay.Interaction;
using Game.Gameplay.SurvivalMeсhanics.Fatigue;
using Game.UI.Pages;
using Game.WorldMap;
using Infrastructure;
using Infrastructure.GameplayStates;
using Zenject;

namespace Game.Gameplay
{
    public class SleighInteractive : UpgradingInteractiveObject
    {
        private GameplayLocalStateMachine _stateMachine;
        private WorldMapController _worldMap;
        private CursorInteractor _cursorInteractor;
        private FatigueRadialButtonsHelper _helper;

        [Inject]
        private void Init(GameplayLocalStateMachine stateMachine, WorldMapController worldMap,
            CursorInteractor cursorInteractor, FatigueRadialButtonsHelper helper)
        {
            _stateMachine = stateMachine;
            _worldMap = worldMap;
            _cursorInteractor = cursorInteractor;
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

        public void OpenSortiePage()
        {
            UIManager.OpenPage<SortiePage>();
        }

        public void ToMapState()
        {
            _stateMachine.Enter<MapState>();
            _worldMap.Open();
            _cursorInteractor.Open();
        }
    }
}