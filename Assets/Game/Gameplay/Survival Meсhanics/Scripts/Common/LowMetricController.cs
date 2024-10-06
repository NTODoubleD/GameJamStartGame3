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
        
        public bool IsEffectActive { get; private set; }

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
            if (newValue <= 0 && IsEffectActive == false)
                ActivateEffect();
            else if (newValue > 0 && IsEffectActive)
                DeactivateEffect();
        }

        private void ActivateEffect()
        {
            IsEffectActive = true;
            _lowMetricEffectController.AddEffect(GetName(), _damage);
            OnEffectActivated();
        }

        private void DeactivateEffect()
        {
            IsEffectActive = false;
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