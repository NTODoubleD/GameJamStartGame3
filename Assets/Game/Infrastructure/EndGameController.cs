using System.Linq;
using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.Gameplay;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using Game.UI.Pages;
using UnityEngine;

namespace Game.Infrastructure
{
    public class EndGameController : MonoBehaviour
    {
        private DayCycleController _dayCycleController;
        private Herd _herd;
        private IUIManager _iuManager;

        private void Awake()
        {
            _dayCycleController = Services.SceneContext.GetModule<DayCycleController>();
            _herd = Services.SceneContext.GetModule<Herd>();
            _iuManager = Services.ProjectContext.GetModule<IUIManager>();
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
                _iuManager.OpenPage<EndGamePage>();
            }
        }
    }
}