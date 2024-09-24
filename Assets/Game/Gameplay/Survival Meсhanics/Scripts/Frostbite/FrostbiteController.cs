using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Character;
using Game.Gameplay.SurvivalMechanics;

namespace Game.Gameplay.SurvivalMeсhanics.Frostbite
{
    public class FrostbiteController
    {
        private readonly PlayerMetricsModel _playerMetricsModel;
        private readonly CharacterMovementSettings _characterMovementSettings;
        private readonly FrostbiteConfig _config;

        private CancellationTokenSource _cts;
        private bool _isEffectActive;

        public FrostbiteController(PlayerMetricsModel playerMetricsModel,
            CharacterMovementSettings characterMovementSettings, FrostbiteConfig config)
        {
            _playerMetricsModel = playerMetricsModel;
            _characterMovementSettings = characterMovementSettings;
            _config = config;
            _playerMetricsModel.HeatResistanceChanged += OnHeatResistanceChanged;
        }
        
        private void OnHeatResistanceChanged(int heatResistance)
        {
            if (heatResistance <= 0 && _isEffectActive == false)
                ActivateEffect();
            else if (heatResistance > 0 && _isEffectActive)
                DeactivateEffect();
        }

        private void ActivateEffect()
        {
            _cts = new CancellationTokenSource();
            _characterMovementSettings.CanSprint = false;
            ApplyDamageAsync(_cts.Token).Forget();
        }

        private void DeactivateEffect()
        {
            _characterMovementSettings.CanSprint = true;
            _cts.Cancel();
            _cts.Dispose();
        }

        private async UniTask ApplyDamageAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested && _playerMetricsModel.Health > 0)
            {
                _playerMetricsModel.Health -= _config.Damage;
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }
        
        ~FrostbiteController()
        {
            _playerMetricsModel.HeatResistanceChanged -= OnHeatResistanceChanged;
            
            if (_cts != null)
                _cts.Dispose();
        }
    }
}