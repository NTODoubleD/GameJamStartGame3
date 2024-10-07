using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.WorldMap
{
    public class MapCameraController : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 10;
        [SerializeField] private float _dragSensitivity = 10;
        [SerializeField] private Collider _cameraCollider;

        [Space, SerializeField] private float _animationDuration = 1;
        [SerializeField] private float _zoomDistance = 20;
        [SerializeField] private AnimationCurve _enableAnimationCurve;
        [SerializeField] private Vector3 _startAnimationRotation;

        private Vector3 _homePosition;
        private Quaternion _homeRotation;
        private Quaternion AnimationRotation => Quaternion.Euler(_startAnimationRotation);

        private GameInput _gameInput;
        private Camera _camera;

        [Inject]
        private void Init(GameInput gameInput)
        {
            _gameInput = gameInput;
        }

        private void Awake()
        {
            _homePosition = transform.position;
            _homeRotation = transform.rotation;
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _gameInput.Map.Move.performed += OnMove;
            _gameInput.Map.Move.canceled += OnMove;

            _gameInput.Map.DragClick.performed += OnBeginDragMove;
            _gameInput.Map.DragClick.canceled += OnEndDragMove;

            StartCoroutine(StartAnimation());
        }

        private void OnDisable()
        {
            _gameInput.Map.Move.performed -= OnMove;
            _gameInput.Map.Move.canceled -= OnMove;

            _gameInput.Map.DragClick.performed -= OnBeginDragMove;
            _gameInput.Map.DragClick.canceled -= OnEndDragMove;
        }

        private Vector2 _moveVector;
        private bool _isDragging;
        private Vector2 _dragAnchorPoint;
        private Vector3 _cameraAnchorPosition;

        private void OnMove(InputAction.CallbackContext context)
        {
            _moveVector = context.ReadValue<Vector2>();
        }

        private void OnBeginDragMove(InputAction.CallbackContext obj)
        {
            _dragAnchorPoint = _camera.ScreenToViewportPoint(_gameInput.Map.Point.ReadValue<Vector2>());
            _cameraAnchorPosition = transform.position;
            _isDragging = true;
        }

        private void OnEndDragMove(InputAction.CallbackContext obj)
        {
            _isDragging = false;
        }

        private void Update()
        {
            if (_isDragging)
            {
                Vector2 currentDrag = _camera.ScreenToViewportPoint(_gameInput.Map.Point.ReadValue<Vector2>());

                Vector2 delta = currentDrag - _dragAnchorPoint;
                Vector3 offset = _dragSensitivity * new Vector3(delta.x, 0, delta.y);

                SetCameraPosition(_cameraAnchorPosition - offset);
                return;
            }

            MoveCamera(_moveVector);
        }

        private void MoveCamera(Vector2 moveVector)
        {
            var cameraPosition = transform.position;

            Vector3 newPosition = cameraPosition +
                                  _sensitivity * Time.deltaTime * new Vector3(moveVector.x, 0, moveVector.y);

            SetCameraPosition(newPosition);
        }

        private void SetCameraPosition(Vector3 position)
        {
            var cameraPosition = transform.position;

            var bounds = _cameraCollider.bounds;

            float x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
            float z = Mathf.Clamp(position.z, bounds.min.z, bounds.max.z);

            cameraPosition = new Vector3(x, cameraPosition.y, z);

            transform.position = cameraPosition;
        }

        private IEnumerator StartAnimation()
        {
            Vector3 startPosition = _homePosition + transform.forward * _zoomDistance;
            transform.position = startPosition;

            float remainingTime = _animationDuration;

            while (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;

                float progress = 1 - remainingTime / _animationDuration;

                transform.position =
                    Vector3.Lerp(startPosition, _homePosition, _enableAnimationCurve.Evaluate(progress));

                transform.rotation =
                    Quaternion.Lerp(AnimationRotation, _homeRotation, _enableAnimationCurve.Evaluate(progress));

                yield return null;
            }

            transform.position = _homePosition;
        }
    }
}