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
            StartFrostAsync().Forget();
        }

        private async UniTask StartFrostAsync()
        {
            _frostController.Enable(FrostLevel.Weak);

            if (_dayCycleController.CurrentDay % _strongFrostSettings.DayPeriod == 0)
                await ProceedFrostAsync(_strongFrostSettings, FrostLevel.Strong);
            else if (_dayCycleController.CurrentDay % _averageFrostSettings.DayPeriod == 0)
                await ProceedFrostAsync(_averageFrostSettings, FrostLevel.Average);
        }
        
        private async UniTask ProceedFrostAsync(FrostConfig.FrostSettings settings, FrostLevel frostLevel)
        {
            await UniTask.Delay(
                settings.StartDelays[Random.Range(0, settings.StartDelays.Length)] * 1000);
                
            _frostController.Enable(frostLevel);
                
            await UniTask.Delay(settings.Duration * 1000);
            
            _frostController.Enable(FrostLevel.Weak);
        }
        
        ~FrostStarter()
        {
            _dayCycleController.DayStarted -= OnDayStarted;
        }
    }
}