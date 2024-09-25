using System;
using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMeсhanics.Frostbite;
using Game.Gameplay.SurvivalMeсhanics.PlayerMetrics;

namespace Game.Gameplay.SurvivalMeсhanics.Dehydration
{
    public class DehydrationController : LowMetricController
    {
        private readonly PlayerMetricsModel _metricsModel;

        public DehydrationController(PlayerMetricsModel metricsModel, FrostbiteConfig config, LowMetricEffectController lowMetricEffectController) : base(config.Damage, lowMetricEffectController)
        {
            _metricsModel = metricsModel;
        }

        protected override void SubscribeOnMetric(Action<float> handler)
        {
            _metricsModel.ThirstChanged += handler;
        }

        protected override void UnsubscribeFromMetric(Action<float> handler)
        {
            _metricsModel.ThirstChanged -= handler;
        }

        protected override string GetName()
        {
            return nameof(DehydrationController);
        }
    }
}