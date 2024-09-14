using System.Collections.Generic;
using DoubleDCore.Attributes;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class TriggerInteractionController : MonoBehaviour
    {
        [SerializeField, ReadOnlyProperty] private InteractionTrigger[] _interactionTriggers;

        private readonly List<InteractionTrigger> _triggersToCheck = new();
        
        private InteractiveObjectsWatcher _objectsWatcher;
        private SceneInteractionData _sceneInteractionData;
        
        [Inject]
        private void Construct(InteractiveObjectsWatcher objectsWatcher, SceneInteractionData sceneInteractionData)
        {
            _objectsWatcher = objectsWatcher;
            _sceneInteractionData = sceneInteractionData;
        }
        
        private void Awake()
        {
            _triggersToCheck.AddRange(_interactionTriggers);
        }

        private void OnValidate()
        {
            _interactionTriggers = FindObjectsOfType<InteractionTrigger>();
        }

        private void OnEnable()
        {
            foreach (var interactionTrigger in _triggersToCheck)
            {
                interactionTrigger.Entered += OnTriggerEntered;
                interactionTrigger.Exited += OnTriggerExited;
            }
        }

        private void OnDisable()
        {
            foreach (var interactionTrigger in _triggersToCheck)
            {
                interactionTrigger.Entered -= OnTriggerEntered;
                interactionTrigger.Exited -= OnTriggerExited;
            }
        }

        private void OnTriggerEntered(InteractionTrigger trigger)
        {
            _objectsWatcher.enabled = false;
            _sceneInteractionData.CurrentObject = trigger.ConnectedObject;
        }
        
        private void OnTriggerExited(InteractionTrigger trigger)
        {
            _sceneInteractionData.CurrentObject = null;
            _objectsWatcher.enabled = true;
        }
    }
}