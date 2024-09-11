using UnityEngine;
using Zenject;

namespace DoubleDCore.Ad.Base
{
    public abstract class RewardingObject : MonoBehaviour
    {
        [SerializeField] private string _rewardID = "";

        public string ID => _rewardID;

        private IAdvertisingService _advertisingService;

        [Inject]
        private void Init(IAdvertisingService advertisingService)
        {
            _advertisingService = advertisingService;
        }

        protected virtual void OnEnable() => _advertisingService.RewardedAdClosed += OnRewarded;

        protected virtual void OnDisable() => _advertisingService.RewardedAdClosed -= OnRewarded;

        private void OnRewarded(bool isSuccess, string id)
        {
            if (isSuccess == false)
                return;

            if (id != ID)
                return;

            GiveReward();
        }

        protected abstract void GiveReward();
    }
}