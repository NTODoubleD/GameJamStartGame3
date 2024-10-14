using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMeсhanics.PlayerMetrics;

namespace Game.Gameplay.SurvivalMeсhanics.Frostbite
{
    public class FrostbiteController : LowMetricController
    {
        private readonly PlayerMetricsModel _metricsModel;

        public FrostbiteController(PlayerMetricsModel metricsModel, FrostbiteConfig config, LowMetricEffectController lowMetricEffectController) : base(config.Damage, lowMetricEffectController)
        {
            _metricsModel = metricsModel;
        }

        protected override void SubscribeOnMetric(System.Action<float> handler)
        {
            _metricsModel.HeatResistanceChanged += handler;
        }

        protected override void UnsubscribeFromMetric(System.Action<float> handler)
        {
            _metricsModel.HeatResistanceChanged -= handler;
        }

        protected override string GetName()
        {
            return nameof(FrostbiteController);
        }
    }
}