using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Character;
using Game.Gameplay.SurvivalMechanics;

namespace Game.Gameplay.SurvivalMeсhanics.PlayerMetrics
{
    public class LowMetricEffectController : IRealtimeSurvivalMechanic
    {
        private readonly PlayerMetricsModel _playerMetricsModel;
        private readonly CharacterMovementSettings _characterMovementSettings;

        private readonly Dictionary<string, int> _effectsDamage = new();

        private CancellationTokenSource _cts;
        private bool _isEffectActive;

        public LowMetricEffectController(PlayerMetricsModel playerMetricsModel,
            CharacterMovementSettings characterMovementSettings)
        {
            _playerMetricsModel = playerMetricsModel;
            _characterMovementSettings = characterMovementSettings;
        }

        public void AddEffect(string name, int damage)
        {
            _effectsDamage[name] = damage;
            Enable();
        }

        public void RemoveEffect(string name)
        {
            _effectsDamage.Remove(name);
            
            if (_effectsDamage.Keys.Count == 0)
                Disable();
        }

        public void Enable()
        {
            if (_effectsDamage.Keys.Count > 0 && _isEffectActive == false)
                ActivateEffect();
        }

        public void Disable()
        {
            if (_isEffectActive)
                DeactivateEffect();
        }

        private void ActivateEffect()
        {
            _isEffectActive = true;
            _cts = new CancellationTokenSource();
            _characterMovementSettings.CanSprint = false;
            ApplyDamageAsync(_cts.Token).Forget();
        }

        private void DeactivateEffect()
        {
            _isEffectActive = false;
            _characterMovementSettings.CanSprint = true;
            _cts.Cancel();
            _cts.Dispose();
        }

        private async UniTask ApplyDamageAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested && _playerMetricsModel.Health > 0)
            {
                int damage = _effectsDamage.Keys.Sum(x => _effectsDamage[x]);
                
                _playerMetricsModel.Health -= damage;
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }
        
        ~LowMetricEffectController()
        {
            if (_cts != null && _isEffectActive)
                _cts.Dispose();
        }
    }
}