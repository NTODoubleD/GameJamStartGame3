﻿using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Crafting;
using Game.Gameplay.DayCycle;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    public class HeatController
    {
        private readonly HeatZone _heatZone;
        private readonly PlayerMetricsModel _playerMetricsModel;
        private readonly HeatConfig _config;
        private readonly CookingController _cookingController;
        private readonly SleepingController _sleepingController;

        private CancellationTokenSource _cts;
        private bool _isHeating;

        public HeatController(HeatZone heatZone, PlayerMetricsModel playerMetricsModel,
            HeatConfig config, CookingController cookingController,
            SleepingController sleepingController)
        {
            _heatZone = heatZone;
            _playerMetricsModel = playerMetricsModel;
            _config = config;
            _cookingController = cookingController;
            _sleepingController = sleepingController;

            _heatZone.Entered += StartHeating;
            _heatZone.Exited += StopHeating;
            _cookingController.CookingStarted += OnCookingStarted;
            _cookingController.CookingEnded += OnCookingEnded;
            _sleepingController.SleepCalled += OnSleepCalled;
        }
        
        private void OnSleepCalled()
        {
            _playerMetricsModel.HeatResistance = _config.HeatRestoreValue;
        }

        private void OnCookingStarted()
        {
            _heatZone.IsEnabled = true;
        }

        private void OnCookingEnded()
        {
            _heatZone.IsEnabled = false;
            StopHeating();
        }

        private void StartHeating()
        {
            if (_isHeating)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            
            _isHeating = true;
            _cts = new CancellationTokenSource();
            HeatAsync(_cts.Token).Forget();
        }

        private void StopHeating()
        {
            if (_isHeating)
            {
                _cts.Cancel();
                _cts.Dispose();
                _isHeating = false;
            }
        }

        private async UniTask HeatAsync(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                await UniTask.Delay(1000);
                _playerMetricsModel.HeatResistance += _config.HeatAddition;
            }
        }

        ~HeatController()
        {
            _heatZone.Entered -= StartHeating;
            _heatZone.Exited -= StopHeating;
            _cookingController.CookingStarted -= OnCookingStarted;
            _cookingController.CookingEnded -= OnCookingEnded;
            _sleepingController.SleepCalled -= OnSleepCalled;

            if (_isHeating)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }
    }
}