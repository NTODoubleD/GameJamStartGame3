using UnityEngine;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class InteractionTipController : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonRoot;

        private SceneInteractionData _sceneInteractionData;

        [Inject]
        private void Construct(SceneInteractionData data)
        {
            _sceneInteractionData = data;
        }
        
        private void OnEnable()
        {
            _sceneInteractionData.ObjectChanged += OnCurrentObjectChanged;
        }

        private void OnDisable()
        {
            _sceneInteractionData.ObjectChanged -= OnCurrentObjectChanged;
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