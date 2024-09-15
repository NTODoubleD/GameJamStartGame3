using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class PlayerInteractionObserver : MonoBehaviour
    {
        private SceneInteractionData _sceneInteractionData;
        private InteractiveObject _current;
        private GameInput _inputController;
        private CharacterMover _player;

        [Inject]
        private void Init(GameInput inputController, SceneInteractionData data, CharacterMover player)
        {
            _inputController = inputController;
            _sceneInteractionData = data;
            _player = player;
        }

        private void OnEnable()
        {
            _sceneInteractionData.ObjectChanged += ChangeCurrentObject;
            _inputController.Player.Interact.started += InteractWithCurrentObject;
        }

        private void OnDisable()
        {
            _sceneInteractionData.ObjectChanged -= ChangeCurrentObject;
            _inputController.Player.Interact.started -= InteractWithCurrentObject;
        }

        private void ChangeCurrentObject(InteractiveObject newObject)
        {
            _current = newObject;
        }

        private void InteractWithCurrentObject(InputAction.CallbackContext callbackContext)
        {
            if (_current != null)
            {
                _player.transform.forward = GetXZDirection(_current.transform, _player.transform);
                _current.Interact();
            }
        }

        private Vector3 GetXZDirection(Transform target, Transform player)
        {
            Vector3 xzTargetPos = target.position;
            xzTargetPos.y = 0;

            Vector3 xzPlayerPos = player.position;
            xzPlayerPos.y = 0;

            return (xzTargetPos - xzPlayerPos).normalized;
        }
    }
}