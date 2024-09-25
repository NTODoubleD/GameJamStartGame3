using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    public class FrostController : IRealtimeSurvivalMechanic
    {
        private readonly IEnumerable<IHeatResistable> _heatResistables;
        private readonly FrostConfig _frostConfig;
        
        private readonly HashSet<IHeatResistable> _whitelist = new();

        private CancellationTokenSource _cts;
        private bool _isEffectEnabled = false;

        public FrostController(IEnumerable<IHeatResistable> heatResistables, FrostConfig frostConfig)
        {
            _heatResistables = heatResistables;
            _frostConfig = frostConfig;
        }

        public void Enable()
        {
            Enable(FrostLevel.Weak);
        }

        public void Enable(FrostLevel effectLevel)
        {
            if (_isEffectEnabled)
                Disable();

            _isEffectEnabled = true;
            _cts = new CancellationTokenSource();
            DoFrostEffect(effectLevel, _cts.Token).Forget();
        }

        public void Disable()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _isEffectEnabled = false;
        }

        public bool AddToWhiteList(IHeatResistable heatResistable)
        {
            return _whitelist.Add(heatResistable);
        }

        public bool RemoveFromWhiteList(IHeatResistable heatResistable)
        {
            return _whitelist.Remove(heatResistable);
        }
        
        private async UniTask DoFrostEffect(FrostLevel effectLevel, CancellationToken token)
        {
            int effectValue = _frostConfig.GetConsumptionValue(effectLevel);
            
            while (!token.IsCancellationRequested)
            {
                foreach (var heatResistable in _heatResistables)
                {
                    if (_whitelist.Contains(heatResistable))
                        continue;
                    
                    heatResistable.HeatResistance -= effectValue;
                }
                
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }
        
        ~FrostController()
        {
            if (_isEffectEnabled)
                Disable();
        }
    }
}