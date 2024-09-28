using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.SnowPicking
{
    public class PlayerSnowPickController : MonoBehaviour
    {
        [SerializeField] private CharacterAnimatorController _characterAnimatorController;
        [SerializeField] private ItemInfo _snowItem;

        private ItemStorage _storage;
        
        [Inject]
        private void Init(ItemStorage storage)
        {
            _storage = storage;
        }

        private void OnEnable()
        {
            _characterAnimatorController.PickedUp += OnPicked;
        }

        private void OnDisable()
        {
            _characterAnimatorController.PickedUp -= OnPicked;
        }

        public void InteractWithSnowPack()
        {
            PickSnow();
        }

        private void PickSnow()
        {
            _characterAnimatorController.AnimatePickingUp();
        }

        private void OnPicked()
        {
            _storage.AddItems(_snowItem, 1);
        }
    }
}