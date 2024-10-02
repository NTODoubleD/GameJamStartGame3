﻿using Game.Gameplay.Interaction;
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

        [Inject]
        private void Init(GameplayLocalStateMachine stateMachine, WorldMapController worldMap,
            CursorInteractor cursorInteractor)
        {
            _stateMachine = stateMachine;
            _worldMap = worldMap;
            _cursorInteractor = cursorInteractor;
        }

        public override void Interact()
        {
            UIManager.OpenPage<RadialMenuPage, RadialMenuArgument>(GetRadialMenuArgument());
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