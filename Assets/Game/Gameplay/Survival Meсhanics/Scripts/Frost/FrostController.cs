using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    public class FrostController
    {
        private readonly PlayerMetricsModel _model;
        private readonly FrostConfig _frostConfig;

        private CancellationTokenSource _cts;

        public FrostController(PlayerMetricsModel model, FrostConfig frostConfig)
        {
            _model = model;
            _frostConfig = frostConfig;
        }

        public void Enable(FrostLevel effectLevel)
        {
            Disable();
            
            _cts = new CancellationTokenSource();
            DoFrostEffect(effectLevel, _cts.Token).Forget();
        }

        public void Disable()
        {
            _cts?.Cancel();
            _cts?.Dispose();
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
            _cts?.Dispose();
        }
    }
}