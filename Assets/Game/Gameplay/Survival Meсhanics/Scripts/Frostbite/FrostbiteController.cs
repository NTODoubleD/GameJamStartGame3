using System.Threading;
using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMeсhanics.PlayerMetrics;

namespace Game.Gameplay.SurvivalMeсhanics.Frostbite
{
    public class FrostbiteController
    {
        private readonly PlayerMetricsModel _playerMetricsModel;
        private readonly FrostbiteConfig _config;
        private readonly LowMetricEffectController _lowMetricEffectController;

        private CancellationTokenSource _cts;
        private bool _isEffectActive;

        public FrostbiteController(PlayerMetricsModel playerMetricsModel,
            FrostbiteConfig config, LowMetricEffectController lowMetricEffectController)
        {
            _playerMetricsModel = playerMetricsModel;
            _config = config;
            _lowMetricEffectController = lowMetricEffectController;
            
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
            _isEffectActive = true;
            _lowMetricEffectController.AddEffect(nameof(FrostbiteController), _config.Damage);
        }

        private void DeactivateEffect()
        {
            _isEffectActive = false;
            _lowMetricEffectController.RemoveEffect(nameof(FrostbiteController));
        }
        
        ~FrostbiteController()
        {
            _playerMetricsModel.HeatResistanceChanged -= OnHeatResistanceChanged;
        }
    }
}