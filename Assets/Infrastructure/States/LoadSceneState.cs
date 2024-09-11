using System;
using Cysharp.Threading.Tasks;
using DoubleDCore.Automat.Base;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<LoadScenePayload>
    {
        public async void Enter(LoadScenePayload payload)
        {
            payload.BeforeLoad?.Invoke();

            var operation = SceneManager.LoadSceneAsync(payload.SceneName);

            await operation.ToUniTask();

            payload.AfterLoad?.Invoke();
        }

        public void Exit()
        {
        }
    }

    public record LoadScenePayload(
        string SceneName,
        Action BeforeLoad = null,
        Action AfterLoad = null)
    {
        public string SceneName { get; } = SceneName;
        public Action BeforeLoad { get; } = BeforeLoad;
        public Action AfterLoad { get; } = AfterLoad;
    }
}