using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public class InteractionTipController : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectsWatcher _objectsWatcher;
        [SerializeField] private GameObject _buttonRoot;

        private void OnEnable()
        {
            _objectsWatcher.CurrentChanged += OnCurrentObjectChanged;
        }

        private void OnDisable()
        {
            _objectsWatcher.CurrentChanged -= OnCurrentObjectChanged;
        }

        private void OnCurrentObjectChanged(InteractiveObject obj)
        {
            if (obj == null)
                _buttonRoot.SetActive(false);
            else
                _buttonRoot.SetActive(true);
        }
    }
}