using UnityEngine;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class HighlightInteractiveObjectsController : MonoBehaviour
    {
        private SceneInteractionData _sceneInteractionData;
        private InteractiveObject _lastObject;

        [Inject]
        private void Construct(SceneInteractionData data)
        {
            _sceneInteractionData = data;
        }

        private void OnEnable()
        {
            _sceneInteractionData.ObjectChanged += OnCurrentObjectToInteractChanged;
        }

        private void OnDisable()
        {
            _sceneInteractionData.ObjectChanged -= OnCurrentObjectToInteractChanged;
        }

        public void DisableCurrentHighlight()
        {
            if (_lastObject != null)
                _lastObject.DisableHighlight();

            _lastObject = null;
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