using DoubleDTeam.Attributes;
using DoubleDTeam.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.Interaction
{
    public class InteractiveObjectsWatcher : MonoBehaviour
    {
        private readonly HashSet<InteractiveObject> _interactiveObjectsToCheck = new();
        private readonly List<InteractiveObject> _objectsInRange = new();

        [SerializeField] private DistancePlayerToObjectChecker _distanceChecker;
        [SerializeField, ReadOnlyProperty] private InteractiveObject[] _interactiveObjects;

        public InteractiveObject CurrentObject { get; private set; }

        public event UnityAction<InteractiveObject> CurrentChanged;

        private void OnValidate()
        {
            _interactiveObjects = FindObjectsOfType<InteractiveObject>();
            _interactiveObjectsToCheck.AddRange(_interactiveObjects);
        }

        public void AddObjectToWatch(InteractiveObject obj)
        {
            _interactiveObjectsToCheck.Add(obj);
        }

        private void Update()
        {
            _objectsInRange.Clear();

            foreach (var interactiveObject in _interactiveObjectsToCheck)
                if (_distanceChecker.IsPlayerInRange(interactiveObject.transform))
                    _objectsInRange.Add(interactiveObject);

            if (_objectsInRange.Count == 0)
            {
                CurrentObject = null;
                CurrentChanged?.Invoke(CurrentObject);
                return;
            }

            InteractiveObject closestObject = _objectsInRange.OrderBy(x => _distanceChecker.GetDistanceToPlayer(x.transform)).First();
            Debug.Log(closestObject.name);

            if (closestObject != CurrentObject)
            {
                CurrentObject = closestObject;
                CurrentChanged?.Invoke(closestObject);
            }
        }
    }
}