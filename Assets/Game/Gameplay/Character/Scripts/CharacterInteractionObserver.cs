using Game.Gameplay.Interaction;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Character
{
    public class CharacterInteractionObserver : MonoBehaviour
    {
        [SerializeField] private CharacterAnimatorController _animatorController;
        [SerializeField] private InteractiveObjectsWatcher _objectsWatcher;

        private SceneInteractionData _sceneInteractionData;
        private TriggerInteractionController _triggerInteractionController;

        [Inject]
        private void Construct(SceneInteractionData data, TriggerInteractionController triggerInteractionController)
        {
            _sceneInteractionData = data;
            _triggerInteractionController = triggerInteractionController;
        }

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
            _triggerInteractionController.IsEnabled = false;
            _sceneInteractionData.CurrentObject = null;
        }

        private void OnEndedInteraction()
        {
            _objectsWatcher.enabled = true;
            _triggerInteractionController.IsEnabled = true;
        }
    }
}