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

        private void Awake()
        {
            _interactiveObjectsToCheck.AddRange(_interactiveObjects);
        }

        private void OnDisable()
        {
            CurrentObject = null;
            CurrentChanged?.Invoke(CurrentObject);
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
            _objectsInRange.Clear();

            foreach (var interactiveObject in _interactiveObjectsToCheck)
                if (_distanceChecker.IsPlayerInRange(interactiveObject))
                    _objectsInRange.Add(interactiveObject);

            if (_objectsInRange.Count == 0)
            {
                CurrentObject = null;
                CurrentChanged?.Invoke(CurrentObject);
                return;
            }

            InteractiveObject closestObject = _objectsInRange.OrderBy(x => _distanceChecker.GetDistanceToPlayer(x.transform)).First();

            if (closestObject != CurrentObject)
            {
                CurrentObject = closestObject;
                CurrentChanged?.Invoke(closestObject);
            }
        }
    }
}