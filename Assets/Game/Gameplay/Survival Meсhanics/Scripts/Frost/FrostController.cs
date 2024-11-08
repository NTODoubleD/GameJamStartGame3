using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    public class FrostController : IRealtimeSurvivalMechanic
    {
        public bool IsEffectEnabled { get; private set; }  = false;
        public FrostLevel CurrentFrostLevel { get; private set; }
        
        private readonly IEnumerable<IHeatResistable> _heatResistables;
        private readonly FrostConfig _frostConfig;
        private readonly HashSet<IHeatResistable> _whitelist = new();

        private CancellationTokenSource _cts;
        private bool _isPaused;
        
        public event Action<FrostLevel> FrostLevelChanged; 

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
            if (IsEffectEnabled)
                Disable();

            IsEffectEnabled = true;
            CurrentFrostLevel = effectLevel;
            _cts = new CancellationTokenSource();
            DoFrostEffect(_cts.Token).Forget();
            
            FrostLevelChanged?.Invoke(CurrentFrostLevel);
        }

        public void Disable()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            IsEffectEnabled = false;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }

        public bool AddToWhiteList(IHeatResistable heatResistable)
        {
            return _whitelist.Add(heatResistable);
        }

        public bool RemoveFromWhiteList(IHeatResistable heatResistable)
        {
            return _whitelist.Remove(heatResistable);
        }
        
        private async UniTask DoFrostEffect(CancellationToken token)
        {
            float effectValue = _frostConfig.GetConsumptionValue(CurrentFrostLevel);
            
            while (!token.IsCancellationRequested)
            {
                if (_isPaused == false)
                {
                    foreach (var heatResistable in _heatResistables)
                    {
                        if (_whitelist.Contains(heatResistable))
                            continue;
                    
                        heatResistable.HeatResistance -= effectValue;
                    }
                }
                
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }
        
        ~FrostController()
        {
            if (IsEffectEnabled)
                Disable();
        }
    }
}