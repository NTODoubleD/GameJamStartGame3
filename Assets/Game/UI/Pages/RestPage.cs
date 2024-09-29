using DG.Tweening;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.SurvivalMechanics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Pages
{
    public class RestPage : MonoPage, IUIPage
    {
        [SerializeField] private UIMetric _heatResistanceMetric;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _text;

        private PlayerMetricsModel _playerMetrics;
        
        [Inject]
        private void Init(PlayerMetricsModel playerMetrics)
        {
            _playerMetrics = playerMetrics;
        }
        
        public void Open()
        {
            _playerMetrics.HeatResistanceChanged += OnHeatResistanceChanged;

            _background.DOFade(0.6f, 1).OnComplete(() =>
            {
                _text.gameObject.SetActive(true);
                _heatResistanceMetric.gameObject.SetActive(true);
                
                _heatResistanceMetric.Refresh(_playerMetrics.HeatResistance);
            });
            
            SetCanvasState(true);
        }

        private void OnHeatResistanceChanged(float newValue)
        {
            _heatResistanceMetric.Refresh(newValue);
        }

        public override void Close()
        {
            _playerMetrics.HeatResistanceChanged -= OnHeatResistanceChanged;

            _background.DOFade(0, 1).OnComplete(() => SetCanvasState(false));
            _text.gameObject.SetActive(false);
            _heatResistanceMetric.gameObject.SetActive(false);
        }
    }
}