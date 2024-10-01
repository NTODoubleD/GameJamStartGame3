using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    public class HeatController
    {
        private readonly HeatZone _heatZone;
        private readonly PlayerMetricsModel _playerMetricsModel;
        private readonly HeatConfig _config;

        private CancellationTokenSource _cts;
        private bool _isHeating;

        public HeatController(HeatZone heatZone, PlayerMetricsModel playerMetricsModel, HeatConfig config)
        {
            _heatZone = heatZone;
            _playerMetricsModel = playerMetricsModel;
            _config = config;

            _heatZone.Entered += OnPlayerEntered;
            _heatZone.Exited += OnPlayerExited;
        }

        private void OnPlayerEntered()
        {
            if (_isHeating)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            
            _isHeating = true;
            _cts = new CancellationTokenSource();
            HeatAsync(_cts.Token).Forget();
        }

        private void OnPlayerExited()
        {
            if (_isHeating)
            {
                _cts.Cancel();
                _cts.Dispose();
                _isHeating = false;
            }
        }

        private async UniTask HeatAsync(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                await UniTask.Delay(1000);
                _playerMetricsModel.HeatResistance += _config.HeatAddition;
            }
        }

        ~HeatController()
        {
            _heatZone.Entered -= OnPlayerEntered;
            _heatZone.Exited -= OnPlayerExited;

            if (_isHeating)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }
    }
}