using System;
using System.Threading;
using Game.Gameplay.SurvivalMeсhanics.Frostbite;
using Game.Gameplay.SurvivalMeсhanics.PlayerMetrics;

namespace Game.Gameplay.SurvivalMechanics
{
    public abstract class LowMetricController
    {
        private readonly int _damage;
        private readonly LowMetricEffectController _lowMetricEffectController;

        private CancellationTokenSource _cts;
        private bool _isEffectActive;

        public LowMetricController(int damage, LowMetricEffectController lowMetricEffectController)
        {
            _damage = damage;
            _lowMetricEffectController = lowMetricEffectController;
            
            SubscribeOnMetric(OnMetricChanged);
        }
        
        private void OnMetricChanged(int newValue)
        {
            if (newValue <= 0 && _isEffectActive == false)
                ActivateEffect();
            else if (newValue > 0 && _isEffectActive)
                DeactivateEffect();
        }

        private void ActivateEffect()
        {
            _isEffectActive = true;
            _lowMetricEffectController.AddEffect(GetName(), _damage);
        }

        private void DeactivateEffect()
        {
            _isEffectActive = false;
            _lowMetricEffectController.RemoveEffect(GetName());
        }
        
        protected abstract void SubscribeOnMetric(Action<int> handler);
        protected abstract void UnsubscribeFromMetric(Action<int> handler);
        protected abstract string GetName();
        
        ~LowMetricController()
        {
            UnsubscribeFromMetric(OnMetricChanged);
        }
    }
}