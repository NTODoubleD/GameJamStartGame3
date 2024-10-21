using DoubleDCore.UI.Base;
using Game.UI.Pages;
using Zenject;

namespace Game.UI
{
    public class WikiPageOpener : ButtonListener
    {
        private IUIManager _uiManager;

        [Inject]
        private void Init(IUIManager uiManager)
        {
            _uiManager = uiManager;
        }

        protected override void OnButtonClicked()
        {
            _uiManager.OpenPage<WikiPage>();
        }
    }
}