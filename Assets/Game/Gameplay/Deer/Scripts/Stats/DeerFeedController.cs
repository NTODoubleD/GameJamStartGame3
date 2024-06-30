using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using Game.Gameplay.Feeding;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using System;
using UnityEngine;

namespace Game.Gameplay.Deers
{
    public class DeerFeedController : MonoModule
    {
        [SerializeField] private CharacterAnimatorController _characterAnimatorController;
        [SerializeField] private PlayerMossPickController _mossController;
        [SerializeField] private ItemInfo _feedItem;
        [SerializeField] private float _hungerRegenerationPerItem = 0.1f;

        [Header("Settings")]
        [SerializeField] private FeedStat[] _feedStats;

        private ItemStorage _storage;
        private Deer _lastDeer;

        private void Awake()
        {
            _storage = Services.ProjectContext.GetModule<ItemStorage>();
        }

        public bool CanFeed(Deer deer)
        {
            if (deer.DeerInfo.Status == DeerStatus.Dead)
                return false;

            if (_storage.GetCount(_feedItem) > 0)
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
                int maximumItemsToFeed = (int)(hungerNeed / _hungerRegenerationPerItem);
                int resultItemsCount = Mathf.Min(totalFeedItemCount, maximumItemsToFeed);

                _storage.RemoveItems(_feedItem, resultItemsCount);
                _characterAnimatorController.AnimateFeeding(() => ApplyFeed(deer, resultItemsCount));
            }
            else
            {
                Debug.LogError("CAN'T FEED THID DEER");
            }
        }

        private void ApplyFeed(Deer deer, int count)
        {
            deer.DeerInfo.HungerDegree = Mathf.Min(1, count * _hungerRegenerationPerItem);
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