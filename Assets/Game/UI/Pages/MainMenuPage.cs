using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Infrastructure;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class MainMenuPage : MonoPage, IUIPage
    {
        private GameStateMachine _stateMachine;
        private GlobalConfig _config;
        private IUIManager _uiManager;

        [Inject]
        private void Init(GameStateMachine stateMachine, IResourcesContainer resourcesContainer, IUIManager uiManager)
        {
            _stateMachine = stateMachine;
            _config = resourcesContainer.GetResource<ConfigsResource>().GetConfig<GlobalConfig>();
            _uiManager = uiManager;
        }

        public override void Initialize()
        {
            SetCanvasState(true);
        }

        public void Open()
        {
            SetCanvasState(true);
        }

        public void StartNewGame()
        {
            var payload = new LoadScenePayload(_config.LyricsSceneName,
                AfterLoad: () => _stateMachine.Enter<LyricsState>());

            _stateMachine.Enter<LoadSceneState, LoadScenePayload>(payload);
        }

        public void OpenTutorial()
        {
            _uiManager.OpenPage<TutorialPage>();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}