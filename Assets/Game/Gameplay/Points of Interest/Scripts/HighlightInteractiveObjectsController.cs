using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public class HighlightInteractiveObjectsController : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectsWatcher _objectsWatcher;

        private InteractiveObject _lastObject;

        private void OnEnable()
        {
            _objectsWatcher.CurrentChanged += OnCurrentObjectToInteractChanged;
        }

        private void OnDisable()
        {
            _objectsWatcher.CurrentChanged -= OnCurrentObjectToInteractChanged;
        }

        private void OnCurrentObjectToInteractChanged(InteractiveObject newObject)
        {
            if (_lastObject == newObject)
                return;

            if (_lastObject != null)
                _lastObject.DisableHighlight();

            if (newObject != null)
                newObject.EnableHighlight();

            _lastObject = newObject;
        }
    }
}