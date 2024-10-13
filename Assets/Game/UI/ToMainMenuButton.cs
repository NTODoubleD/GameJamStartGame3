using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using DoubleDCore.UI.Base;
using Infrastructure;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class ToMainMenuButton : ButtonListener
    {
        private GameStateMachine _stateMachine;
        private GlobalConfig _config;

        [Inject]
        private void Init(GameStateMachine stateMachine, IResourcesContainer resourcesContainer)
        {
            _stateMachine = stateMachine;
            _config = resourcesContainer.GetResource<ConfigsResource>().GetConfig<GlobalConfig>();
        }

        protected override void OnButtonClicked()
        {
            Time.timeScale = 1;
            _stateMachine.Enter<LoadSceneState, LoadScenePayload>(
                new LoadScenePayload(_config.MainMenuSceneName,
                    AfterLoad: () => _stateMachine.Enter<MainMenuState>()));
        }
    }
}