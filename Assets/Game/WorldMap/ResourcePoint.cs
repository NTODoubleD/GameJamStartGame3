using System;
using DoubleDCore.UI.Base;
using Game.Gameplay.Buildings;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Items;
using Game.UI.Pages;
using UnityEngine;
using Zenject;

namespace Game.WorldMap
{
    public class ResourcePoint : WorldInterestPoint
    {
        [SerializeField] private SortieResourceArgument _sortieResource;

        private IUIManager _uiManager;
        private WorldMapController _worldMap;
        private CursorInteractor _cursorInteractor;
        private DayCycleController _cycleController;
        private SleighBuildingReference _sleighBuilding;

        public SortieResourceArgument SortieResource => _sortieResource;

        [Inject]
        private void Init(IUIManager uiManager, WorldMapController worldMapController,
            CursorInteractor cursorInteractor, DayCycleController cycleController,
            SleighBuildingReference sleighBuilding)
        {
            _uiManager = uiManager;
            _worldMap = worldMapController;
            _cursorInteractor = cursorInteractor;
            _cycleController = cycleController;
            _sleighBuilding = sleighBuilding;
        }

        public override void Interact()
        {
            StartSortie();
        }

        public PointInfo GetPointInfo()
        {
            return new PointInfo
            {
                Name = Name,
                SortieResource = _sortieResource,
                SleighLevel = _sleighBuilding.SleighBuilding.CurrentLevel - 1,
                Position = transform.position,
            };
        }

        private void StartSortie()
        {
            _cycleController.DayStarted += CloseWorldMap;

            var sortiePage = _uiManager.GetPage<SortiePage>();

            sortiePage.SetResourcePriorities(SortieResource);
            _uiManager.OpenPage<SortiePage>();
        }

        private void CloseWorldMap()
        {
            _cycleController.DayStarted -= CloseWorldMap;

            _worldMap.Close();
            _cursorInteractor.Close();
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
        [field: SerializeField] public int[] Count { get; private set; } = new int[3];

        public int GetCount(int slightLevel)
        {
            if (slightLevel < 0 || slightLevel >= Count.Length)
                return 0;

            return Count[slightLevel];
        }
    }
}