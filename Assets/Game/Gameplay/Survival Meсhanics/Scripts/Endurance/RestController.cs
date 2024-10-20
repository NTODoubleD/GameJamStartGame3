using System.Threading;
using Cysharp.Threading.Tasks;
using DoubleDCore.UI.Base;
using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMechanics.Frost;
using Game.UI;
using Game.UI.Pages;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Gameplay.SurvivalMeсhanics.Endurance
{
    public class RestController : MonoBehaviour
    {
        private GameObject _player;
        private LocalMenuOpener _localMenuOpener;
        private GameInput _inputController;
        private IUIManager _uiManager;
        private RestConfig _config;
        private PlayerMetricsModel _playerMetrics;
        private FrostController _frostController;

        private CancellationTokenSource _cts;
        private bool _isResting;

        [Inject]
        private void Init(CharacterMover playerMover, LocalMenuOpener localMenuOpener,
            GameInput inputController, IUIManager uiManager, RestConfig config,
            PlayerMetricsModel playerMetricsModel, FrostController frostController)
        {
            _player = playerMover.gameObject;
            _localMenuOpener = localMenuOpener;
            _inputController = inputController;
            _uiManager = uiManager;
            _config = config;
            _playerMetrics = playerMetricsModel;
            _frostController = frostController;
        }

        public void StartResting()
        {
            _uiManager.ClosePage<PlayerMetricsPage>();
            _uiManager.ClosePage<ResourcePage>();
            _uiManager.ClosePage<QuestPage>();
            _uiManager.OpenPage<RestPage>();

            _player.SetActive(false);
            _localMenuOpener.enabled = false;
            _inputController.UI.CloseMenu.performed += OnEscapePerfomed;

            _frostController.AddToWhiteList(_playerMetrics);
            _isResting = true;

            _cts = new CancellationTokenSource();
            RestAsync(_cts.Token).Forget();
        }

        private async UniTask RestAsync(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                await UniTask.Delay(1000, cancellationToken: token);
                _playerMetrics.HeatResistance += _config.RestoreValue;
            }
        }

        private void OnEscapePerfomed(InputAction.CallbackContext obj)
        {
            _inputController.UI.CloseMenu.performed -= OnEscapePerfomed;
            EndResting();
        }

        private void EndResting()
        {
            _cts.Cancel();
            _cts.Dispose();

            _isResting = false;
            _frostController.RemoveFromWhiteList(_playerMetrics);

            _uiManager.ClosePage<RestPage>();
            _uiManager.OpenPage<PlayerMetricsPage>();
            _uiManager.OpenPage<ResourcePage>();
            _uiManager.OpenPage<QuestPage>();

            _player.SetActive(true);
            _localMenuOpener.enabled = true;

            _inputController.Player.Enable();
            _inputController.UI.Disable();
        }

        private void OnDestroy()
        {
            if (_isResting)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }
    }
}