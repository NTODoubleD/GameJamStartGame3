using System.Linq;
using DoubleDCore.UI.Base;
using Game.Gameplay;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using Game.Gameplay.SurvivalMechanics;
using Game.UI.Pages;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EndGameController : MonoBehaviour
    {
        private DayCycleController _dayCycleController;
        private Herd _herd;
        private IUIManager _uiManager;
        private PlayerMetricsModel _playerMetrics;
        
        [Inject]
        private void Init(DayCycleController dayCycleController, PlayerMetricsModel playerMetrics, Herd herd, IUIManager uiManager)
        {
            _dayCycleController = dayCycleController;
            _herd = herd;
            _uiManager = uiManager;
            _playerMetrics = playerMetrics;
        }

        private void OnEnable()
        {
            _dayCycleController.DayStarted += OnDayStarted;
            _playerMetrics.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _dayCycleController.DayStarted -= OnDayStarted;
            _playerMetrics.HealthChanged -= OnHealthChanged;
        }

        private void OnDayStarted()
        {
            if (_herd.CurrentHerd.Count(d => d.DeerInfo.Gender == GenderType.Male) <= 0
                || _herd.CurrentHerd.Count(d => d.DeerInfo.Gender == GenderType.Female) <= 0)
                EndGame();
        }
        
        private void OnHealthChanged(int health)
        {
            if (health <= 0)
                EndGame();
        }
        
        private void EndGame()
        {
            _uiManager.OpenPage<EndGamePage>();
        }
    }
}