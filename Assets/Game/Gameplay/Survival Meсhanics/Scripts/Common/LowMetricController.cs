using System;
using System.Threading;
using Game.Gameplay.SurvivalMeсhanics.PlayerMetrics;
using Zenject;

namespace Game.Gameplay.SurvivalMechanics
{
    public abstract class LowMetricController : IInitializable, IDisposable
    {
        private readonly float _damage;
        private readonly LowMetricEffectController _lowMetricEffectController;

        private CancellationTokenSource _cts;
        private bool _isEffectActive;

        public LowMetricController(float damage, LowMetricEffectController lowMetricEffectController)
        {
            _damage = damage;
            _lowMetricEffectController = lowMetricEffectController;
        }
        
        public void Initialize()
        {
            SubscribeOnMetric(OnMetricChanged);
        }
        
        public void Dispose()
        {
            UnsubscribeFromMetric(OnMetricChanged);
        }
        
        private void OnMetricChanged(float newValue)
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
            OnEffectActivated();
        }

        private void DeactivateEffect()
        {
            _isEffectActive = false;
            _lowMetricEffectController.RemoveEffect(GetName());
            OnEffectDeactivated();
        }
        
        protected abstract void SubscribeOnMetric(Action<float> handler);
        protected abstract void UnsubscribeFromMetric(Action<float> handler);
        protected abstract string GetName();
        
        protected virtual void OnEffectActivated()
        {
        }
        
        protected virtual void OnEffectDeactivated()
        {
        }
    }
}