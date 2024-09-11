using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using System;
using System.Linq;
using DoubleDCore.Service;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game.Gameplay.Deers
{
    public class DeerHealController : MonoService
    {
        [SerializeField] private CharacterAnimatorController _characterAnimatorController;
        [SerializeField] private ItemInfo _healItem;
        [SerializeField] private SickItemsCondition[] _sickItems;

        private ItemStorage _storage;

        public event UnityAction<Deer> Healed;

        [Inject]
        private void Init(ItemStorage storage)
        {
            _storage = storage;
        }

        public bool CanHeal(Deer deer)
        {
            if (deer.DeerInfo.Status == DeerStatus.Standard || deer.DeerInfo.IsDead)
                return false;

            return _sickItems.First(x => x.Status == deer.DeerInfo.Status).NeccessaryItemCount <=
                   _storage.GetCount(_healItem);
        }

        public void Heal(Deer deer)
        {
            if (CanHeal(deer))
            {
                int itemsToRemove = _sickItems.First(x => x.Status == deer.DeerInfo.Status).NeccessaryItemCount;
                _storage.RemoveItems(_healItem, itemsToRemove);
                _characterAnimatorController.AnimateHealing(() => ApplyHeal(deer));
            }
            else
            {
                Debug.LogError("CAN'T HEAL DEER");
            }
        }

        private void ApplyHeal(Deer deer)
        {
            deer.DeerInfo.Status = DeerStatus.Standard;
            Healed?.Invoke(deer);
        }

        [Serializable]
        private class SickItemsCondition
        {
            [SerializeField] private DeerStatus _sickStatus;
            [SerializeField] private int _neccessaryItemCount;

            public DeerStatus Status => _sickStatus;
            public int NeccessaryItemCount => _neccessaryItemCount;
        }
    }
}