using System.Collections;
using DoubleDCore.Automat.Base;
using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using DoubleDCore.Tween.Base;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.States
{
    public class LyricsState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IResourcesContainer _resourcesContainer;

        private AsyncOperation _currentOperation;

        private bool _sceneReady;
        private bool _userReady;
        private readonly GameInput _gameInput;
        private readonly ICoroutineRunner _coroutineRunner;

        [Inject]
        public LyricsState(GameStateMachine stateMachine, IResourcesContainer resourcesContainer, GameInput gameInput,
            ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _resourcesContainer = resourcesContainer;
            _gameInput = gameInput;
            _coroutineRunner = coroutineRunner;
        }

        public void Enter()
        {
            _sceneReady = false;
            _userReady = false;

            _gameInput.Enable();
            _gameInput.UI.Enable();

            var config = _resourcesContainer.GetResource<ConfigsResource>().GetConfig<GlobalConfig>();

            _coroutineRunner.StartCoroutine(LoadScene(config.GameSceneName));
        }

        public void Exit()
        {
        }

        public void OnLyricsEnd()
        {
            _userReady = true;

            if (_sceneReady)
                EnterGameplay();
        }

        private void OnSceneLoad(AsyncOperation asyncOperation)
        {
            _currentOperation = asyncOperation;
            _sceneReady = true;

            if (_userReady)
                EnterGameplay();
        }

        private void EnterGameplay()
        {
            _stateMachine.Enter<GameplayState>();
            _currentOperation.allowSceneActivation = true;
        }

        IEnumerator LoadScene(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            while (asyncOperation.isDone == false)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    OnSceneLoad(asyncOperation);
                    yield break;
                }

                yield return null;
            }
        }
    }
}