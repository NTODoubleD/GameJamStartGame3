using DoubleDCore.Initialization;
using DoubleDCore.UI.Base;
using Zenject;

namespace DoubleDCore.UI
{
    public class PageBinder : MonoInitializer
    {
        private IUIManager _uiManager;
        private MonoPage _page;

        [Inject]
        private void Init(IUIManager uiManager)
        {
            _uiManager = uiManager;
        }

        private void Awake()
        {
            Initialize();
        }

        public override void Initialize()
        {
            _page = GetComponent<MonoPage>();

            if (_page == null)
                return;

            if (_uiManager.ContainsPage(_page))
                return;

            _uiManager.RegisterPageByType(_page);
        }

        public override void Deinitialize()
        {
            if (_page == null)
                return;

            _uiManager.RemovePage(_page);
        }
    }
}