using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.WorldMap
{
    public class MapCameraController : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 10;
        [SerializeField] private Collider _cameraCollider;

        private GameInput _gameInput;

        [Inject]
        private void Init(GameInput gameInput)
        {
            _gameInput = gameInput;
        }

        private void OnEnable()
        {
            _gameInput.Map.Move.performed += OnMove;
            _gameInput.Map.Move.canceled += OnMove;
        }

        private void OnDisable()
        {
            _gameInput.Map.Move.performed -= OnMove;
            _gameInput.Map.Move.canceled -= OnMove;
        }

        private Vector2 _moveVector;

        private void OnMove(InputAction.CallbackContext context)
        {
            _moveVector = context.ReadValue<Vector2>();
        }

        private void Update()
        {
            var cameraPosition = transform.position;

            Vector3 newPosition = cameraPosition +
                                  _sensitivity * Time.deltaTime * new Vector3(_moveVector.x, 0, _moveVector.y);

            var bounds = _cameraCollider.bounds;

            float x = Mathf.Clamp(newPosition.x, bounds.min.x, bounds.max.x);
            float z = Mathf.Clamp(newPosition.z, bounds.min.z, bounds.max.z);

            cameraPosition = new Vector3(x, cameraPosition.y, z);

            transform.position = cameraPosition;
        }
    }
}