using DoubleDCore.UI.Base;
using Game.Quests.Base;
using Zenject;

namespace Game.Quests
{
    public abstract class OpenUIPageTask<TPageType> : YakutSubTask where TPageType : IPage
    {
        private IUIManager _uiManager;

        [Inject]
        private void Init(IUIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public override void Play()
        {
            _uiManager.PageOpened += UiManagerOnPageOpened;
        }

        public override void Close()
        {
            _uiManager.PageOpened -= UiManagerOnPageOpened;
        }

        private void UiManagerOnPageOpened(IPage page)
        {
            if (page is TPageType)
                Progress = 1;
        }
    }
}