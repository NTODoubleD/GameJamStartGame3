using DoubleDCore.Service;
using DoubleDCore.UI.Base;
using Game.UI.Pages;
using Infrastructure;
using Infrastructure.GameplayStates;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.WorldMap
{
    public class CursorInteractor : MonoService
    {
        [SerializeField] private LayerMask _intractableLayer;
        [SerializeField] private SortiePage _sortiePage;

        private Camera _camera;
        private GameInput _gameInput;
        private IUIManager _uiManager;
        private GameplayLocalStateMachine _stateMachine;
        private WorldMapController _worldMap;

        [Inject]
        private void Init(GameInput gameInput, IUIManager uiManager, GameplayLocalStateMachine stateMachine,
            WorldMapController worldMapController)
        {
            _gameInput = gameInput;
            _uiManager = uiManager;
            _stateMachine = stateMachine;
            _worldMap = worldMapController;
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Open()
        {
            _gameInput.Map.Click.performed += OnClick;
        }

        public void Close()
        {
            _gameInput.Map.Click.performed -= OnClick;

            _uiManager.ClosePage<WorldInterestPointPage>();
            _isPageOpened = false;
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            var ray = _camera.ScreenPointToRay(_gameInput.Map.Point.ReadValue<Vector2>());

            if (Physics.Raycast(ray, out var hit, 200, _intractableLayer) == false)
                return;

            var intractable = hit.collider.GetComponent<WorldInterestPoint>();

            if (intractable == null)
                return;

            OpenPointPage(intractable);
        }

        private bool _isPageOpened;

        private void OpenPointPage(WorldInterestPoint point)
        {
            if (_isPageOpened)
                _uiManager.ClosePage<WorldInterestPointPage>();

            _uiManager.OpenPage<WorldInterestPointPage, InterestPointArgument>(new InterestPointArgument
            {
                Name = point.Name,
                SortieResource = point.SortieResource,
                Position = point.transform.position,
                StartSortieCallback = StartSortie
            });

            _isPageOpened = true;
        }

        private void StartSortie(SortieResourceArgument context)
        {
            _stateMachine.Enter<PlayerMovingState>();
            _worldMap.Close();
            Close();

            _sortiePage.SetResourcePriorities(context);
            _sortiePage.StartSortie();
        }
    }
}