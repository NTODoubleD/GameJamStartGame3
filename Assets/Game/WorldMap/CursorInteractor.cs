using DoubleDCore.Service;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.WorldMap
{
    public class CursorInteractor : MonoService
    {
        [SerializeField] private LayerMask _intractableLayer;

        private Camera _camera;
        private GameInput _gameInput;

        [Inject]
        private void Init(GameInput gameInput)
        {
            _gameInput = gameInput;
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
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            var ray = _camera.ScreenPointToRay(_gameInput.Map.Point.ReadValue<Vector2>());

            if (Physics.Raycast(ray, out var hit, 200, _intractableLayer) == false)
                return;

            var intractable = hit.collider.GetComponent<WorldInterestPoint>();

            if (intractable == null)
                return;

            intractable.Interact();
        }
    }
}