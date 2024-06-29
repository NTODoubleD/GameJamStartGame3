using DoubleDTeam.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighlightInteractiveObjectsController : MonoBehaviour
{
    private readonly HashSet<InteractiveObject> _interactiveObjectsToCheck = new();
    private readonly List<InteractiveObject> _objectsInRange = new();

    [SerializeField] private DistancePlayerToObjectChecker _distanceChecker;
    [SerializeField] private InteractiveObject[] _interactiveObjects;

    private InteractiveObject _lastObject;

    private void OnValidate()
    {
        _interactiveObjects = FindObjectsOfType<InteractiveObject>();
        _interactiveObjectsToCheck.AddRange(_interactiveObjects);
    }

    public void AddObject(InteractiveObject obj)
    {
        _interactiveObjectsToCheck.Add(obj);
    }

    private void Update()
    {
        _objectsInRange.Clear();

        foreach (var interactiveObject in _interactiveObjects)
            if (_distanceChecker.IsPlayerInRange(interactiveObject.transform))
                _objectsInRange.Add(interactiveObject);

        if (_objectsInRange.Count == 0)
        {
            DisabelLastObject();
            return;
        }

        InteractiveObject closestObject = _objectsInRange.OrderBy(x => _distanceChecker.GetSqrDistanceToPlayer(x.transform)).First();

        if (closestObject != _lastObject)
        {
            DisabelLastObject();
            _lastObject = closestObject;
            closestObject.EnableHighlight();
        }
    }

    private void DisabelLastObject()
    {
        if (_lastObject != null)
        {
            _lastObject.DisableHighlight();
            _lastObject = null;
        }
    }
}