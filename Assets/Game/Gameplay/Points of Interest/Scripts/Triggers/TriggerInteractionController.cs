using System.Collections.Generic;
using DoubleDCore.Attributes;
using DoubleDCore.Service;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class TriggerInteractionController : MonoService
    {
        public bool IsEnabled = true;
        
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
                interactionTrigger.Stayed += OnTriggerStayed;
            }
        }

        private void OnDisable()
        {
            foreach (var interactionTrigger in _triggersToCheck)
            {
                interactionTrigger.Entered -= OnTriggerEntered;
                interactionTrigger.Exited -= OnTriggerExited;
                interactionTrigger.Stayed -= OnTriggerStayed;
            }
        }

        private void OnTriggerEntered(InteractionTrigger trigger)
        {
            if (IsEnabled == false)
                return;
            
            _objectsWatcher.enabled = false;
            _sceneInteractionData.CurrentObject = trigger.ConnectedObject;
        }
        
        private void OnTriggerExited(InteractionTrigger trigger)
        {
            if (IsEnabled == false)
                return;
            
            _sceneInteractionData.CurrentObject = null;
            _objectsWatcher.enabled = true;
        }

        private void OnTriggerStayed(InteractionTrigger trigger)
        {
            if (IsEnabled == false)
                return;
            
            if (_sceneInteractionData.CurrentObject != trigger.ConnectedObject)
            {
                OnTriggerEntered(trigger);
            }
        }
    }
}