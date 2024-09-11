using System;

namespace DoubleDCore.Ad.Base
{
    public interface IAdvertisingService
    {
        public bool IsAdblockEnabled { get; }

        public bool IsFullscreenAvailable { get; }
        public bool IsPreloaderAvailable { get; }
        public bool IsRewardedAvailable { get; }
        public bool IsStickyAvailable { get; }

        public bool IsFullscreenPlaying { get; }
        public bool IsPreloaderPlaying { get; }
        public bool IsRewardPlaying { get; }
        public bool IsStickyPlaying { get; }

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

        public void ShowPreloader(Action onPreloaderStart = null, Action<bool> onPreloaderClose = null);

        public void ShowFullscreen(Action onFullscreenStart = null, Action<bool> onFullscreenClose = null);

        public void ShowRewarded(string id = "", Action onRewardedStart = null,
            Action<bool, string> onRewardedClose = null);

        public void ShowSticky();

        public void RefreshSticky();

        public void CloseSticky();
    }
}