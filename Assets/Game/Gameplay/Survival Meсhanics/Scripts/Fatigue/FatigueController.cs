using System;
using Game.Gameplay.Character;
using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMeсhanics.PlayerMetrics;

namespace Game.Gameplay.SurvivalMeсhanics.Fatigue
{
    public class FatigueController : LowMetricController
    {
        private readonly FatigueConfig _config;
        private readonly PlayerMetricsModel _playerMetricsModel;
        private readonly CharacterMovementSettings _movementSettings;

        public FatigueController(FatigueConfig config, PlayerMetricsModel playerMetricsModel,
            CharacterMovementSettings movementSettings, LowMetricEffectController lowMetricEffectController)
            : base(0, lowMetricEffectController)
        {
            _config = config;
            _playerMetricsModel = playerMetricsModel;
            _movementSettings = movementSettings;
        }

        protected override void SubscribeOnMetric(Action<float> handler)
        {
            _playerMetricsModel.EnduranceChanged += handler;
        }

        protected override void UnsubscribeFromMetric(Action<float> handler)
        {
            _playerMetricsModel.EnduranceChanged -= handler;
        }

        protected override void OnEffectActivated()
        {
            _movementSettings.SpeedMultiplyer *= _config.SpeedMultiplier;
        }
        
        protected override void OnEffectDeactivated()
        {
            _movementSettings.SpeedMultiplyer /= _config.SpeedMultiplier;
        }

        protected override string GetName()
        {
            return nameof(FatigueController);
        }
    }
}