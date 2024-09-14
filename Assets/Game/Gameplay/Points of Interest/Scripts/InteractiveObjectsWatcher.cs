using System.Collections.Generic;
using System.Linq;
using DoubleDCore.Attributes;
using DoubleDCore.Service;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class InteractiveObjectsWatcher : MonoService
    {
        private List<InteractiveObject> _interactiveObjectsToCheck = new();
        private readonly List<InteractiveObject> _objectsInRange = new();

        [SerializeField] private DistancePlayerToObjectChecker _distanceChecker;
        [SerializeField, ReadOnlyProperty] private InteractiveObject[] _interactiveObjects;

        private SceneInteractionData _sceneInteractionData;
        
        [Inject]
        private void Construct(SceneInteractionData sceneInteractionData)
        {
            _sceneInteractionData = sceneInteractionData;
        }

        private void Awake()
        {
            _interactiveObjectsToCheck.AddRange(_interactiveObjects.Where(x => x.InteractedByTrigger == false));
        }

        private void OnValidate()
        {
            _interactiveObjects = FindObjectsOfType<InteractiveObject>();
        }

        public void AddObjectToWatch(InteractiveObject obj)
        {
            _interactiveObjectsToCheck.Add(obj);
        }

        private void Update()
        {
            RefreshWatchList();
            _objectsInRange.Clear();

            foreach (var interactiveObject in _interactiveObjectsToCheck)
                if (_distanceChecker.IsPlayerInRange(interactiveObject))
                    _objectsInRange.Add(interactiveObject);

            if (_objectsInRange.Count == 0)
            {
                _sceneInteractionData.CurrentObject = null;
                return;
            }

            InteractiveObject closestObject =
                _objectsInRange.OrderBy(x => _distanceChecker.GetDistanceToPlayer(x.transform)).First();

            if (closestObject != _sceneInteractionData.CurrentObject)
            {
                _sceneInteractionData.CurrentObject = closestObject;
            }
        }

        private void RefreshWatchList()
        {
            _interactiveObjectsToCheck = _interactiveObjectsToCheck.Where(x => x != null).ToList();
        }
    }
}