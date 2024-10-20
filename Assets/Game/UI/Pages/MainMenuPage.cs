﻿using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Infrastructure;
using Infrastructure.States;
using Zenject;

namespace Game.UI.Pages
{
    public class MainMenuPage : MonoPage, IUIPage
    {
        private GameStateMachine _stateMachine;
        private GlobalConfig _config;

        [Inject]
        private void Init(GameStateMachine stateMachine, IResourcesContainer resourcesContainer)
        {
            _stateMachine = stateMachine;
            _config = resourcesContainer.GetResource<ConfigsResource>().GetConfig<GlobalConfig>();
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
    }
}