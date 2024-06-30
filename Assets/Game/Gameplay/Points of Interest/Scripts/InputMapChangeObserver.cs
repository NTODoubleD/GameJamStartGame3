using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using DoubleDTeam.InputSystem.Base;
using Game.InputMaps;
using Game.UI.Pages;
using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public class InputMapChangeObserver : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectsWatcher _interactiveObjectsWatcher;

        private InputController _inputController;

        private void Awake()
        {
            _inputController = Services.ProjectContext.GetModule<InputController>();
        }

        private void OnEnable()
        {
            _inputController.InputMapChanged += OnInputMapChanged;
        }

        private void OnDisable()
        {
            _inputController.InputMapChanged -= OnInputMapChanged;
        }

        private void OnInputMapChanged(InputMap newMap)
        {
            if (newMap is UIInputMap)
                _interactiveObjectsWatcher.enabled = false;
            else
                _interactiveObjectsWatcher.enabled = true;
        }
    }
}