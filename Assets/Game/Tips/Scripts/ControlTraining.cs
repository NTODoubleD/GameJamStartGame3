﻿using System.Threading;
using Cysharp.Threading.Tasks;
using DoubleDCore.UI.Base;
using Game.UI.Pages;
using UnityEngine;
using Zenject;

namespace Game.Tips
{
    public class ControlTraining : MonoBehaviour
    {
        [SerializeField] private float _trainingDelay;

        private IUIManager _uiManager;
        private GameInput _gameInput;

        [Inject]
        private void Init(IUIManager uiManager, GameInput gameInput)
        {
            _uiManager = uiManager;
            _gameInput = gameInput;
        }

        private CancellationTokenSource _cancellationToken;

        private async void Start()
        {
            _cancellationToken = new CancellationTokenSource();

            await UniTask.WaitForSeconds(_trainingDelay);

            if (_cancellationToken.IsCancellationRequested)
                return;

            _uiManager.OpenPage<ControlTipPage, ControlTipArgument>(new ControlTipArgument(0, IsWasdCompleted));
        }

        private void OnDestroy()
        {
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
        }

        private bool IsWasdCompleted()
        {
            if (_gameInput.Player.Move.ReadValue<Vector2>().magnitude > 0 == false)
                return false;

            ExecuteNextTip();
            return true;
        }

        private async void ExecuteNextTip()
        {
            await UniTask.WaitForSeconds(_trainingDelay);

            if (_cancellationToken.IsCancellationRequested)
                return;

            _uiManager.OpenPage<ControlTipPage, ControlTipArgument>(new ControlTipArgument(1, IsSprintCompleted));
        }

        private bool IsSprintCompleted()
        {
            if (_gameInput.Player.Move.ReadValue<Vector2>().magnitude > 0 &&
                _gameInput.Player.Sprint.IsPressed())
            {
                ClosePage();
                return true;
            }

            return false;
        }

        private async void ClosePage()
        {
            await UniTask.WaitForSeconds(_trainingDelay);

            if (_cancellationToken.IsCancellationRequested)
                return;

            _uiManager.ClosePage<ControlTipPage>();
        }
    }
}