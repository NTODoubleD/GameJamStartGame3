using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using Game.Quests.Base;
using UnityEngine;
using Zenject;

namespace Game.Quests
{
    public class CollectResourceTask : YakutSubTask
    {
        [SerializeField] private ItemInfo _resource;
        [SerializeField] private bool _countResourcesBefore;

        private ItemStorage _storage;

        [Inject]
        private void Init(ItemStorage storage)
        {
            _storage = storage;
        }

        public override void Play()
        {
            _storage.ItemAdded += OnItemAdded;

            if (_countResourcesBefore)
                OnItemAdded(_resource, _storage.GetCount(_resource));
        }

        public override void Close()
        {
            _storage.ItemAdded -= OnItemAdded;
        }

        private void OnItemAdded(ItemInfo info, int amount)
        {
            if (info.ID == _resource.ID)
                Progress += amount;
        }
    }
}