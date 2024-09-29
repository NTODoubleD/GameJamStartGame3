using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.SurvivalMechanics;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class PlayerMetricsPage : MonoPage, IUIPage
    {
        [SerializeField] private UIMetric _health;
        [SerializeField] private UIMetric _heatResistance;
        [SerializeField] private UIMetric _hunger;
        [SerializeField] private UIMetric _thirst;
        [SerializeField] private UIMetric _endurance;

        private PlayerMetricsModel _playerMetrics;

        [Inject]
        private void Init(PlayerMetricsModel playerMetrics)
        {
            _playerMetrics = playerMetrics;
        }

        public override void Initialize()
        {
            Open();
        }

        public void Open()
        {
            _health.Initialize(_playerMetrics.Health);
            _heatResistance.Initialize(_playerMetrics.HeatResistance);
            _hunger.Initialize(_playerMetrics.Hunger);
            _thirst.Initialize(_playerMetrics.Thirst);
            _endurance.Initialize(_playerMetrics.Endurance);

            _playerMetrics.HealthChanged += OnHealthChanged;
            _playerMetrics.HeatResistanceChanged += OnHeatResistanceChanged;
            _playerMetrics.HungerChanged += OnHungerChanged;
            _playerMetrics.ThirstChanged += OnThirstChanged;
            _playerMetrics.EnduranceChanged += OnEnduranceChanged;

            SetCanvasState(true);
        }

        public override void Close()
        {
            _playerMetrics.HealthChanged -= OnHealthChanged;
            _playerMetrics.HeatResistanceChanged -= OnHeatResistanceChanged;
            _playerMetrics.HungerChanged -= OnHungerChanged;
            _playerMetrics.ThirstChanged -= OnThirstChanged;
            _playerMetrics.EnduranceChanged -= OnEnduranceChanged;

            SetCanvasState(false);
        }

        private void OnHealthChanged(float newValue) =>
            _health.Refresh(newValue);

        private void OnHeatResistanceChanged(float newValue) =>
            _heatResistance.Refresh(newValue);

        private void OnHungerChanged(float newValue) =>
            _hunger.Refresh(newValue);

        private void OnThirstChanged(float newValue) =>
            _thirst.Refresh(newValue);

        private void OnEnduranceChanged(float newValue) =>
            _endurance.Refresh(newValue);
    }
}