using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay.DayCycle;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    public class FrostStarter : IInitializable
    {
        private readonly FrostController _frostController;
        private readonly DayCycleController _dayCycleController;
        private readonly FrostConfig _frostConfig;
        
        private readonly FrostConfig.FrostSettings _strongFrostSettings;
        private readonly FrostConfig.FrostSettings _averageFrostSettings;

        private CancellationTokenSource _cts;
        private bool _isCtsDisposed = true;
        private int _daysFromLastStrongFrost;
        
        public float CurrentFrostTimeLeft { get; private set; }
        public float CurrentFrostDuration { get; private set; }

        public FrostStarter(FrostController frostController, DayCycleController dayCycleController,
            FrostConfig frostConfig)
        {
            _frostController = frostController;
            _dayCycleController = dayCycleController;
            _frostConfig = frostConfig;
            
            _strongFrostSettings = _frostConfig.GetFrostSettings(FrostLevel.Strong);
            _averageFrostSettings = _frostConfig.GetFrostSettings(FrostLevel.Average);

            _dayCycleController.DayStarted += OnDayStarted;
        }
        
        public void Initialize()
        {
            OnDayStarted();
        }

        private void OnDayStarted()
        {
            _daysFromLastStrongFrost++;

            if (_isCtsDisposed == false)
            {
                _cts.Cancel();
                _cts.Dispose();
                _isCtsDisposed = true;
            }

            _isCtsDisposed = false;
            _cts = new CancellationTokenSource();
            StartFrostAsync(_cts.Token).Forget();
        }

        private async UniTask StartFrostAsync(CancellationToken token)
        {
            _frostController.Enable(FrostLevel.Weak);

            if (_daysFromLastStrongFrost >= _strongFrostSettings.DayPeriod)
            {
                await ProceedFrostAsync(_strongFrostSettings, FrostLevel.Strong, token);
            }
            else if (_dayCycleController.CurrentDay % _averageFrostSettings.DayPeriod == 0)
            {
                await ProceedFrostAsync(_averageFrostSettings, FrostLevel.Average, token);
            }
        }
        
        private async UniTask ProceedFrostAsync(FrostConfig.FrostSettings settings, FrostLevel frostLevel, CancellationToken token)
        {
            await UniTask.Delay(
                settings.StartDelays[Random.Range(0, settings.StartDelays.Length)] * 1000, cancellationToken: token);
            
            if (token.IsCancellationRequested)
                return;

            if (frostLevel == FrostLevel.Strong)
                _daysFromLastStrongFrost = 0;
            
            _frostController.Enable(frostLevel);
            CurrentFrostDuration = settings.Duration;
            CurrentFrostTimeLeft = CurrentFrostDuration;

            while (CurrentFrostTimeLeft > 0 && !token.IsCancellationRequested)
            {
                await UniTask.NextFrame(cancellationToken: token);
                CurrentFrostTimeLeft = Mathf.Max(0, CurrentFrostTimeLeft - Time.deltaTime);
            }
            
            _frostController.Enable(FrostLevel.Weak);
        }
        
        ~FrostStarter()
        {
            _dayCycleController.DayStarted -= OnDayStarted;
            
            if (_isCtsDisposed == false)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }
    }
}