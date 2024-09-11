using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using System;
using DoubleDCore.Service;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Deers
{
    public class DeerCutController : MonoService
    {
        [SerializeField] private CharacterAnimatorController _characterAnimatorController;
        [SerializeField] private LootInfo[] _loot;

        private ItemStorage _storage;

        [Inject]
        private void Init(ItemStorage storage)
        {
            _storage = storage;
        }

        public bool CanCut(Deer deer)
        {
            return deer.DeerInfo.IsDead;
        }

        public void Cut(Deer deer)
        {
            if (CanCut(deer))
            {
                _characterAnimatorController.AnimateCutting(() => ApplyCut(deer));
            }
            else
            {
                Debug.LogError("CAN'T CUT THIS DEER");
            }
        }

        private void ApplyCut(Deer deer)
        {
            deer.Cut();

            foreach (var lootInfo in _loot)
                _storage.AddItems(lootInfo.Item, lootInfo.Count);
        }

        [Serializable]
        private class LootInfo
        {
            [SerializeField] private ItemInfo _item;
            [SerializeField] private int _count;

            public ItemInfo Item => _item;
            public int Count => _count;
        }
    }
}