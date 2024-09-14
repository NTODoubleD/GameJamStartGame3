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

        [Inject]
        private void Init(GameInput inputController, SceneInteractionData data)
        {
            _inputController = inputController;
            _sceneInteractionData = data;
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
                _current.Interact();
        }
    }
}