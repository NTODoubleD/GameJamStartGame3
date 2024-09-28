using DoubleDCore.ObjectPooling;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.SurvivalMechanics;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class PlayerMetricsPage : MonoPage, IUIPage
    {
        [SerializeField] private UIMetric _prefab;
        [SerializeField] private RectTransform _container;

        private readonly Pooler<UIMetric> _uiMetricsPool = new();

        private PlayerMetricsModel _playerMetrics;

        [Inject]
        private void Init(PlayerMetricsModel playerMetrics)
        {
            _playerMetrics = playerMetrics;
        }

        public override void Initialize()
        {
            for (int i = 0; i < 5; i++)
            {
                var inst = Instantiate(_prefab, Vector3.zero, Quaternion.identity, _container);
                _uiMetricsPool.Push(inst);
            }

            Open();
        }

        private UIMetric _health;
        private UIMetric _heatResistance;
        private UIMetric _hunger;
        private UIMetric _thirst;
        private UIMetric _endurance;

        public void Open()
        {
            _health = _uiMetricsPool.Get();
            _heatResistance = _uiMetricsPool.Get();
            _hunger = _uiMetricsPool.Get();
            _thirst = _uiMetricsPool.Get();
            _endurance = _uiMetricsPool.Get();

            _health.Initialize(null, _playerMetrics.Health);
            _heatResistance.Initialize(null, _playerMetrics.HeatResistance);
            _hunger.Initialize(null, _playerMetrics.Hunger);
            _thirst.Initialize(null, _playerMetrics.Thirst);
            _endurance.Initialize(null, _playerMetrics.Endurance);

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

            _uiMetricsPool.Return(_health);
            _uiMetricsPool.Return(_heatResistance);
            _uiMetricsPool.Return(_hunger);
            _uiMetricsPool.Return(_thirst);
            _uiMetricsPool.Return(_endurance);

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