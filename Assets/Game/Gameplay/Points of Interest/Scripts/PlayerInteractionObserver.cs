using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class PlayerInteractionObserver : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectsWatcher _objectsWatcher;

        private InteractiveObject _current;

        private GameInput _inputController;

        [Inject]
        private void Init(GameInput inputController)
        {
            _inputController = inputController;
        }

        private void OnEnable()
        {
            _objectsWatcher.CurrentChanged += ChangeCurrentObject;
            _inputController.Player.Interact.started += InteractWithCurrentObject;
        }

        private void OnDisable()
        {
            _objectsWatcher.CurrentChanged -= ChangeCurrentObject;
            _inputController.Player.Interact.started -= InteractWithCurrentObject;
        }

        private void ChangeCurrentObject(InteractiveObject newObject)
        {
            _current = newObject;
        }

        private void InteractWithCurrentObject(InputAction.CallbackContext callbackContext)
        {
            if (_current != null)
                _current.Interact();
        }
    }
}