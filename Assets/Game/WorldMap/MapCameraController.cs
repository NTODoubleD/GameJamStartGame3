using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.WorldMap
{
    public class MapCameraController : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 10;
        [SerializeField] private Collider _cameraCollider;

        [Space, SerializeField] private float _animationDuration = 1;
        [SerializeField] private float _zoomDistance = 20;
        [SerializeField] private AnimationCurve _enableAnimationCurve;
        [SerializeField] private Vector3 _startAnimationRotation;

        private Vector3 _homePosition;
        private Quaternion _homeRotation;
        private Quaternion _animationRotation => Quaternion.Euler(_startAnimationRotation);

        private GameInput _gameInput;

        [Inject]
        private void Init(GameInput gameInput)
        {
            _gameInput = gameInput;
        }

        private void Awake()
        {
            _homePosition = transform.position;
            _homeRotation = transform.rotation;
        }

        private void OnEnable()
        {
            _gameInput.Map.Move.performed += OnMove;
            _gameInput.Map.Move.canceled += OnMove;

            StartCoroutine(StartAnimation());
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
                    Quaternion.Lerp(_animationRotation, _homeRotation, _enableAnimationCurve.Evaluate(progress));

                yield return null;
            }

            transform.position = _homePosition;
        }
    }
}