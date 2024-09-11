using System.Linq;
using DoubleDCore.UI.Base;
using Game.Gameplay;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
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

        [Inject]
        private void Init(DayCycleController dayCycleController, Herd herd, IUIManager uiManager)
        {
            _dayCycleController = dayCycleController;
            _herd = herd;
            _uiManager = uiManager;
        }

        private void OnEnable()
        {
            _dayCycleController.DayStarted += OnDayStarted;
        }

        private void OnDisable()
        {
            _dayCycleController.DayStarted -= OnDayStarted;
        }

        private void OnDayStarted()
        {
            if (_herd.CurrentHerd.Count(d => d.DeerInfo.Gender == GenderType.Male) <= 0
                || _herd.CurrentHerd.Count(d => d.DeerInfo.Gender == GenderType.Female) <= 0)
            {
                _uiManager.OpenPage<EndGamePage>();
            }
        }
    }
}