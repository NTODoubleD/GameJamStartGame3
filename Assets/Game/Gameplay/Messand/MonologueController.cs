using System.Collections.Generic;
using DoubleDCore.PhysicsTools.CollisionImpacts;
using DoubleDCore.UI.Base;
using Game.Gameplay.DayCycle;
using Game.Monologue;
using Game.UI.Pages;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Messand
{
    public class MonologueController : TriggerListener<MonologueTrigger>
    {
        [SerializeField] private List<MonologueGroupInfo> _history;

        private IUIManager _uiManager;
        private DayCycleController _dayController;

        private readonly Queue<MonologueGroupInfo> _historyQueue = new();

        [Inject]
        private void Init(DayCycleController dayCycleController, IUIManager uiManager)
        {
            _dayController = dayCycleController;
            _uiManager = uiManager;
        }

        private void Awake()
        {
            foreach (var monologueInfo in _history)
                _historyQueue.Enqueue(monologueInfo);
        }

        private void OnEnable()
        {
            _dayController.DayStarted += OnDayStarted;
        }

        private void OnDisable()
        {
            _dayController.DayStarted -= OnDayStarted;
        }

        private void OnDayStarted()
        {
            _canPlay = true;
        }

        private bool _canPlay = true;

        protected override bool IsTarget(Collider col, out MonologueTrigger target)
        {
            return col.TryGetComponent(out target);
        }

        protected override void OnTriggerStart(MonologueTrigger target)
        {
            if (_canPlay == false)
                return;

            PlayMonologue();
            _canPlay = false;
        }

        private void PlayMonologue()
        {
            if (_historyQueue.TryDequeue(out var info) == false)
                return;

            _uiManager.OpenPage<MessagePage, MonologueGroupInfo>(info);
        }

        protected override void OnTriggerEnd(MonologueTrigger target)
        {
        }
    }
}