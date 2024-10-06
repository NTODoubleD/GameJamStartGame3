using System;
using DoubleDCore.UI.Base;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Items;
using Game.UI.Pages;
using Infrastructure;
using Infrastructure.GameplayStates;
using UnityEngine;
using Zenject;

namespace Game.WorldMap
{
    public class ResourcePoint : WorldInterestPoint
    {
        [SerializeField] private SortieResourceArgument _sortieResource;

        private IUIManager _uiManager;
        private GameplayLocalStateMachine _stateMachine;
        private WorldMapController _worldMap;
        private CursorInteractor _cursorInteractor;
        private DayCycleController _cycleController;

        public SortieResourceArgument SortieResource => _sortieResource;

        [Inject]
        private void Init(IUIManager uiManager, GameplayLocalStateMachine stateMachine,
            WorldMapController worldMapController, CursorInteractor cursorInteractor,
            DayCycleController cycleController)
        {
            _uiManager = uiManager;
            _stateMachine = stateMachine;
            _worldMap = worldMapController;
            _cursorInteractor = cursorInteractor;
            _cycleController = cycleController;
        }

        public override void Interact()
        {
            if (_uiManager.GetPage<WorldInterestPointPage>().IsDisplayed)
                _uiManager.ClosePage<WorldInterestPointPage>();

            _uiManager.OpenPage<WorldInterestPointPage, InterestPointArgument>(new InterestPointArgument
            {
                Name = Name,
                SortieResource = _sortieResource,
                Position = transform.position,
                StartSortieCallback = StartSortie
            });
        }

        private void StartSortie(SortieResourceArgument context)
        {
            _uiManager.ClosePage<WorldInterestPointPage>();

            _stateMachine.Enter<PlayerMovingState>();
            _cursorInteractor.Close();

            _cycleController.DayStarted += CloseWorldMap;

            var sortiePage = _uiManager.GetPage<SortiePage>();

            sortiePage.SetResourcePriorities(context);
            sortiePage.StartSortie();
        }

        private void CloseWorldMap()
        {
            _cycleController.DayStarted -= CloseWorldMap;
            _worldMap.Close();
        }
    }

    [Serializable]
    public class SortieResourceArgument
    {
        [field: SerializeField] public ResourcePriority Wood { get; private set; }
        [field: SerializeField] public ResourcePriority Moss { get; private set; }
        [field: SerializeField] public ResourcePriority HealGrass { get; private set; }
    }

    [Serializable]
    public class ResourcePriority
    {
        [field: SerializeField] public GameItemInfo Item { get; private set; }
        [field: Range(0, 10), SerializeField] public int Priority { get; private set; }
    }
}