using Game.Gameplay.Interaction;
using UnityEngine;

namespace Game.Gameplay.Feeding
{
    public class PlayerMossPickController : MonoBehaviour
    {
        [SerializeField] private PlayerResourceView _playerMoleView;
        [SerializeField] private CharacterAnimatorController _characterAnimatorController;
        [SerializeField] private InteractiveObjectsWatcher _objectsWatcher;

        public bool IsMossPicked { get; private set; }

        private void OnEnable()
        {
            _characterAnimatorController.StartedPickingUp += OnStartedActualPicking;
            _characterAnimatorController.PickedUp += OnPicked;
        }

        private void OnDisable()
        {
            _characterAnimatorController.StartedPickingUp -= OnStartedActualPicking;
            _characterAnimatorController.PickedUp -= OnPicked;
        }

        public void InteractWithMossPack()
        {
            if (IsMossPicked)
                PutMossBack();
            else
                PickMoss();
        }

        private void PutMossBack()
        {
            IsMossPicked = false;
            _playerMoleView.Disable();
        }

        private void PickMoss()
        {
            IsMossPicked = true;
            _objectsWatcher.enabled = false;
            _characterAnimatorController.AnimatePickingUp();
        }

        private void OnStartedActualPicking()
        {
            _playerMoleView.Enable();
        }

        private void OnPicked()
        {
            _objectsWatcher.enabled = true;
        }
    }
}