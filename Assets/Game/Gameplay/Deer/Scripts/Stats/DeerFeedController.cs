using Game.Gameplay.Feeding;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using System;
using DoubleDCore.Service;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Deers
{
    public class DeerFeedController : MonoService
    {
        [SerializeField] private CharacterAnimatorController _characterAnimatorController;
        [SerializeField] private PlayerMossPickController _mossController;
        [SerializeField] private ItemInfo _feedItem;
        [SerializeField] private float _hungerRegenerationPerItem = 0.1f;

        private ItemStorage _storage;

        [Inject]
        private void Init(ItemStorage storage)
        {
            _storage = storage;
        }

        public bool CanFeed(Deer deer)
        {
            if (deer.DeerInfo.IsDead)
                return false;

            if (_storage.GetCount(_feedItem) == 0)
                return false;

            if (_mossController.IsMossPicked == false)
                return false;

            if (deer.DeerInfo.HungerDegree == 1)
                return false;

            return true;
        }

        public void Feed(Deer deer)
        {
            if (CanFeed(deer))
            {
                int totalFeedItemCount = _storage.GetCount(_feedItem);
                float hungerNeed = 1 - deer.DeerInfo.HungerDegree;
                int maximumItemsToFeed = Mathf.RoundToInt(hungerNeed / _hungerRegenerationPerItem);
                int resultItemsCount = Mathf.Min(totalFeedItemCount, maximumItemsToFeed);

                _storage.RemoveItems(_feedItem, resultItemsCount);
                deer.AnimatorController.StartEat();
                _characterAnimatorController.AnimateFeeding(() => ApplyFeed(deer, resultItemsCount));
            }
            else
            {
                Debug.LogError("CAN'T FEED THID DEER");
            }
        }

        private void ApplyFeed(Deer deer, int count)
        {
            deer.DeerInfo.HungerDegree = Mathf.Min(1, deer.DeerInfo.HungerDegree + count * _hungerRegenerationPerItem);
        }

        [Serializable]
        private class FeedStat
        {
            [SerializeField, Range(0, 100)] private int _hungerPercentage;
            [SerializeField] private int _feedItemCount;

            public float HungerPartCondition => _hungerPercentage / 100f;
            public int FeedItemCount => _feedItemCount;
        }
    }
}