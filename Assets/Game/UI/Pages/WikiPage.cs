using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.UI.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Pages
{
    public class WikiPage : MonoPage, IUIPage
    {
        [SerializeField] private UIWikiMenuItem _prefab;
        [SerializeField] private RectTransform _container;
        [SerializeField] private Vector2 _trainingPageOffset;
        [SerializeField] private ClickButton _closeButton;
        [SerializeField] private Scrollbar _scrollbar;

        private TrainingConfigs _config;
        private IUIManager _uiManager;
        private GameInput _gameInput;

        [Inject]
        private void Init(IResourcesContainer resourcesContainer, IUIManager uiManager, GameInput gameInput)
        {
            _config = resourcesContainer.GetResource<ConfigsResource>().GetConfig<TrainingConfigs>();
            _uiManager = uiManager;
            _gameInput = gameInput;
        }

        private readonly List<UIWikiMenuItem> _menuItems = new();

        public override void Initialize()
        {
            foreach (var trainingInfo in _config.TrainingInfos)
            {
                if (trainingInfo.ShowedInWiki == false)
                    continue;

                var init = Instantiate(_prefab, Vector3.zero, Quaternion.identity, _container);
                init.Initialize(trainingInfo);
                _menuItems.Add(init);
            }

            SetCanvasState(false);
        }

        private UIWikiMenuItem _selectedItem;

        public async void Open()
        {
            _gameInput.UI.Disable();

            foreach (var menuItem in _menuItems)
                menuItem.Clicked += OnButtonClicked;

            _closeButton.Clicked += CloseOnClicked;

            SetCanvasState(true);

            await UniTask.NextFrame();

            _scrollbar.value = 1;
        }

        public override void Close()
        {
            foreach (var menuItem in _menuItems)
                menuItem.Clicked -= OnButtonClicked;

            _closeButton.Clicked -= CloseOnClicked;

            if (_uiManager.GetPage<TrainingPage>().IsDisplayed)
                _uiManager.ClosePage<TrainingPage>();

            _selectedItem?.SetHighlight(false);
            _selectedItem = null;

            _gameInput.UI.Enable();

            SetCanvasState(false);
        }

        private void OnButtonClicked(UIWikiMenuItem menuItem)
        {
            if (_uiManager.GetPage<TrainingPage>().IsDisplayed)
                _uiManager.ClosePage<TrainingPage>();

            _selectedItem?.SetHighlight(false);
            _selectedItem = menuItem;
            _selectedItem.SetHighlight(true);

            _uiManager.OpenPage<TrainingPage, TrainingPageArgument>(
                new TrainingPageArgument
                {
                    TrainingInfo = menuItem.TrainingInfo,
                    Animate = false,
                    Offset = _trainingPageOffset,
                    ShowCloseButton = false
                });
        }

        private void CloseOnClicked()
        {
            Close();
        }
    }
}