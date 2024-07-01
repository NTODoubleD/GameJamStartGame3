﻿using DoubleDTeam.Containers;
using Game.Gameplay.DayCycle;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using System.Collections.Generic;
using DoubleDTeam.UI.Base;
using Game.UI.Pages;
using UnityEngine;

namespace Game.Gameplay.Sleigh
{
    public class SleighReceiveController : MonoBehaviour
    {
        [SerializeField] private DayCycleController _dayCycleController;

        private IReadOnlyDictionary<ItemInfo, int> _currentResult;

        private IUIManager _uiManager;

        private void Awake()
        {
            _uiManager = Services.ProjectContext.GetModule<IUIManager>();
        }

        private void OnEnable()
        {
            _dayCycleController.DayStarted += ShowReceivedSleigh;
        }

        private void OnDisable()
        {
            _dayCycleController.DayStarted -= ShowReceivedSleigh;
        }

        private void ShowReceivedSleigh()
        {
            if (_currentResult == null)
                return;

            var storage = Services.ProjectContext.GetModule<ItemStorage>();

            foreach (var keyPair in _currentResult)
            {
                storage.AddItems(keyPair.Key, keyPair.Value);
                Debug.Log($"RECEIVED {keyPair.Key.Name} {keyPair.Value}");
            }

            _uiManager.OpenPage<ResourceWatcherPage, ResourcePageArgument>(new ResourcePageArgument()
            {
                Label = "Результат вылазки",
                TextHeading = "Получено ресурсов:",
                Resource = new Dictionary<ItemInfo, int>(_currentResult)
            });

            _currentResult = null;
        }

        public void SetReceiveInfo(IReadOnlyDictionary<ItemInfo, int> result)
        {
            _currentResult = result;
        }
    }
}