using System;
using Game.Gameplay.Interaction;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Feeding
{
    public class PlayerMossPickController : MonoBehaviour
    {
        [SerializeField] private PlayerResourceView _playerMoleView;
        [SerializeField] private CharacterAnimatorController _characterAnimatorController;
        [SerializeField] private InteractiveObjectsWatcher _objectsWatcher;
        [SerializeField] private ItemInfo _mossItem;

        private ItemStorage _storage;

        public bool IsMossPicked { get; private set; }

        public event Action MossTaken; 
        public event Action MossPut; 

        [Inject]
        private void Init(ItemStorage storage)
        {
            _storage = storage;
        }

        private void OnEnable()
        {
            _characterAnimatorController.StartedPickingUp += OnStartedActualPicking;
            _characterAnimatorController.PickedUp += OnPicked;
            _storage.ItemRemoved += OnItemRemovedFromStorage;
        }

        private void OnDisable()
        {
            _characterAnimatorController.StartedPickingUp -= OnStartedActualPicking;
            _characterAnimatorController.PickedUp -= OnPicked;
            _storage.ItemRemoved -= OnItemRemovedFromStorage;
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
            MossPut?.Invoke();
        }

        private void PickMoss()
        {
            if (_storage.GetCount(_mossItem) == 0)
                return;
            
            IsMossPicked = true;
            //_objectsWatcher.enabled = false;
            _characterAnimatorController.AnimatePickingUp();
            MossTaken?.Invoke();
        }

        private void OnStartedActualPicking()
        {
            _playerMoleView.Enable();
        }

        private void OnPicked()
        {
            //_objectsWatcher.enabled = true;
        }

        private void OnItemRemovedFromStorage(ItemInfo item, int count)
        {
            if (IsMossPicked && _storage.GetCount(_mossItem) == 0)
            {
                IsMossPicked = false;
                _playerMoleView.Disable();
            }
        }
    }
}