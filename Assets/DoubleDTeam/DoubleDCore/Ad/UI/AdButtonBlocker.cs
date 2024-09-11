using System;
using DoubleDCore.Ad.Base;
using UnityEngine;
using Zenject;

namespace DoubleDCore.Ad.UI
{
    public class AdButtonBlocker : MonoBehaviour
    {
        [SerializeField] private AdType _adButtonType = AdType.Rewarded;

        private IAdvertisingService _advertisingService;

        [Inject]
        private void Init(IAdvertisingService advertisingService)
        {
            _advertisingService = advertisingService;
        }

        private void Awake()
        {
            switch (_adButtonType)
            {
                case AdType.Fullscreen:
                    SetActive(_advertisingService.IsFullscreenAvailable);
                    break;
                case AdType.Rewarded:
                    SetActive(_advertisingService.IsRewardedAvailable);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}