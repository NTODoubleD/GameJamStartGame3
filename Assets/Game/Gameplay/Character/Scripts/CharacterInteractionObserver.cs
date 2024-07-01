using Game.Gameplay.Interaction;
using UnityEngine;

namespace Game.Gameplay.Character
{
    public class CharacterInteractionObserver : MonoBehaviour
    {
        [SerializeField] private CharacterAnimatorController _animatorController;
        [SerializeField] private InteractiveObjectsWatcher _objectsWatcher;

        private void OnEnable()
        {
            _animatorController.StartedInteraction += OnStartedInteraction;
            _animatorController.EndedInteraction += OnEndedInteraction;
        }

        private void OnDisable()
        {
            _animatorController.StartedInteraction -= OnStartedInteraction;
            _animatorController.EndedInteraction -= OnEndedInteraction;
        }

        private void OnStartedInteraction()
        {
            _objectsWatcher.enabled = false;
        }

        private void OnEndedInteraction()
        {
            _objectsWatcher.enabled = true;
        }
    }
}