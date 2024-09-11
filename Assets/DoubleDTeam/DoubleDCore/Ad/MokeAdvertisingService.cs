using System;
using Cysharp.Threading.Tasks;
using DoubleDCore.Ad.Base;
using DoubleDCore.Extensions;
using UnityEngine;

namespace DoubleDCore.Ad
{
    public class MokeAdvertisingService : IAdvertisingService
    {
        public bool IsAdblockEnabled => false;

        public bool IsFullscreenAvailable => true;
        public bool IsPreloaderAvailable => true;
        public bool IsRewardedAvailable => true;
        public bool IsStickyAvailable => true;

        public bool IsFullscreenPlaying { get; private set; } = false;
        public bool IsPreloaderPlaying { get; private set; } = false;
        public bool IsRewardPlaying { get; private set; } = false;
        public bool IsStickyPlaying { get; private set; } = false;

        public event Action AdsStarted;
        public event Action<bool> AdsClosed;

        public event Action PreloaderAdStarted;
        public event Action<bool> PreloaderAdClosed;

        public event Action FullscreenAdStarted;
        public event Action<bool> FullscreenAdClosed;

        public event Action RewardedAdStarted;
        public event Action<bool, string> RewardedAdClosed;

        public event Action StickyStarted;
        public event Action StickyClosed;
        public event Action StickyRefreshed;

        public async void ShowPreloader(Action onPreloaderStart = null, Action<bool> onPreloaderClose = null)
        {
            if (IsPreloaderAvailable == false)
            {
                Log("Preloader not available");
                return;
            }

            Log("Show preloader ad");

            AdsStarted?.Invoke();
            PreloaderAdStarted?.Invoke();

            onPreloaderStart?.Invoke();

            IsPreloaderPlaying = true;
            await UniTask.WaitForSeconds(1f);
            IsPreloaderPlaying = false;

            onPreloaderClose?.Invoke(true);

            PreloaderAdClosed?.Invoke(true);
            AdsClosed?.Invoke(true);

            Log("Preloader ad closed");
        }

        public async void ShowFullscreen(Action onFullscreenStart = null, Action<bool> onFullscreenClose = null)
        {
            if (IsFullscreenAvailable == false)
            {
                Log("Fullscreen not available");
                return;
            }

            Log("Show fullscreen ad");

            AdsStarted?.Invoke();
            FullscreenAdStarted?.Invoke();

            onFullscreenStart?.Invoke();

            IsFullscreenPlaying = true;
            await UniTask.WaitForSeconds(1f);
            IsFullscreenPlaying = false;

            onFullscreenClose?.Invoke(true);

            FullscreenAdClosed?.Invoke(true);
            AdsClosed?.Invoke(true);

            Log("Fullscreen ad closed");
        }

        public async void ShowRewarded(string id = "", Action onRewardedStart = null,
            Action<bool, string> onRewardedClose = null)
        {
            if (IsRewardedAvailable == false)
            {
                Log("Rewarded not available");
                return;
            }

            Log("Show reward ad");

            AdsStarted?.Invoke();
            RewardedAdStarted?.Invoke();

            onRewardedStart?.Invoke();

            IsRewardPlaying = true;
            await UniTask.WaitForSeconds(1f);
            IsRewardPlaying = false;

            onRewardedClose?.Invoke(true, id);

            RewardedAdClosed?.Invoke(true, id);
            AdsClosed?.Invoke(true);

            Log("Reward ad closed");
        }

        public void ShowSticky()
        {
            if (IsStickyAvailable == false)
            {
                Log("Sticky not available");
                return;
            }

            IsStickyPlaying = true;
            Log("Sticky showed");
        }

        public void RefreshSticky()
        {
            if (IsStickyAvailable == false)
            {
                Log("Sticky not available");
                return;
            }

            Log("Sticky refreshed");
        }

        public void CloseSticky()
        {
            if (IsStickyAvailable == false)
            {
                Log("Sticky not available");
                return;
            }

            IsStickyPlaying = false;
            Log("Sticky closed");
        }

        private static void Log(string message)
        {
            Debug.Log(message.Color(new Color(0f, 255f, 135f)));
        }
    }
}