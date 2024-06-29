using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using Game.InputMaps;
using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public class PlayerInteractionObserver : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectsWatcher _objectsWatcher;

        private InteractiveObject _current;
        private PlayerInputMap _input;

        private void Awake()
        {
            _input = Services.ProjectContext.GetModule<InputController>().GetMap<PlayerInputMap>();
        }

        private void OnEnable()
        {
            _objectsWatcher.CurrentChanged += ChangeCurrentObject;
            _input.Interact.Started += InteractWithCurrentObject;
        }

        private void OnDisable()
        {
            _objectsWatcher.CurrentChanged -= ChangeCurrentObject;
            _input.Interact.Started -= InteractWithCurrentObject;
        }

        private void ChangeCurrentObject(InteractiveObject newObject)
        {
            _current = newObject;
        }

        private void InteractWithCurrentObject()
        {
            if (_current != null)
                _current.Interact();
        }
    }
}