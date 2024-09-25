using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMeсhanics.PlayerMetrics;

namespace Game.Gameplay.SurvivalMeсhanics.Exhaustion
{
    public class ExhaustionController : LowMetricController
    {
        private readonly PlayerMetricsModel _metricsModel;

        public ExhaustionController(PlayerMetricsModel metricsModel, ExhaustionConfig config, LowMetricEffectController lowMetricEffectController) : base(config.Damage, lowMetricEffectController)
        {
            _metricsModel = metricsModel;
        }

        protected override void SubscribeOnMetric(System.Action<int> handler)
        {
            _metricsModel.HungerChanged += handler;
        }

        protected override void UnsubscribeFromMetric(System.Action<int> handler)
        {
            _metricsModel.HungerChanged -= handler;
        }

        protected override string GetName()
        {
            return nameof(ExhaustionController);
        }
    }
}