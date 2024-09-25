using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMeсhanics.PlayerMetrics;

namespace Game.Gameplay.Survival_Meсhanics.Scripts.Exhaustion
{
    public class ExhaustionController
    {
        private readonly PlayerMetricsModel _playerMetricsModel;
        private readonly LowMetricEffectController _lowMetricEffectController;
        private readonly ExhaustionConfig _config;

        private bool _isEffectActive;

        public ExhaustionController(PlayerMetricsModel playerMetricsModel,
            LowMetricEffectController lowMetricEffectController, ExhaustionConfig config)
        {
            _playerMetricsModel = playerMetricsModel;
            _lowMetricEffectController = lowMetricEffectController;
            _config = config;

            _playerMetricsModel.HungerChanged += OnHungerChanged;
        }

        private void OnHungerChanged(int hunger)
        {
            if (hunger <= 0 && _isEffectActive == false)
                ActivateEffect();
            else if (hunger > 0 && _isEffectActive)
                DeactivateEffect();
        }

        private void ActivateEffect()
        {
            _isEffectActive = true;
            _lowMetricEffectController.AddEffect(nameof(ExhaustionController), _config.Damage);
        }

        private void DeactivateEffect()
        {
            _isEffectActive = false;
            _lowMetricEffectController.RemoveEffect(nameof(ExhaustionController));
        }

        ~ExhaustionController()
        {
            _playerMetricsModel.HungerChanged -= OnHungerChanged;
        }
    }
}