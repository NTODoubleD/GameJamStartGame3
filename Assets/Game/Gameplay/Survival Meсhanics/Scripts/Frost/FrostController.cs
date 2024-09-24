using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    public class FrostController
    {
        private readonly PlayerMetricsModel _model;
        private readonly FrostConfig _frostConfig;

        private CancellationTokenSource _cts;
        private bool _isEffectEnabled = false;

        public FrostController(PlayerMetricsModel model, FrostConfig frostConfig)
        {
            _model = model;
            _frostConfig = frostConfig;
        }

        public void Enable(FrostLevel effectLevel)
        {
            if (_isEffectEnabled)
                Disable();

            _isEffectEnabled = true;
            _cts = new CancellationTokenSource();
            DoFrostEffect(effectLevel, _cts.Token).Forget();
        }

        public void Disable()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _isEffectEnabled = false;
        }
        
        private async UniTask DoFrostEffect(FrostLevel effectLevel, CancellationToken token)
        {
            int effectValue = _frostConfig.GetConsumptionValue(effectLevel);
            
            while (!token.IsCancellationRequested)
            {
                _model.HeatResistance -= effectValue;
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }
        
        ~FrostController()
        {
            if (_isEffectEnabled)
                Disable();
        }
    }
}