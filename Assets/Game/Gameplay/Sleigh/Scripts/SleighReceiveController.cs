using DoubleDTeam.Containers;
using Game.Gameplay.DayCycle;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Sleigh
{
    public class SleighReceiveController : MonoBehaviour
    {
        [SerializeField] private DayCycleController _dayCycleController;

        private IReadOnlyDictionary<ItemInfo, int> _currentResult;

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

            _currentResult = null;
        }

        public void SetReceiveInfo(IReadOnlyDictionary<ItemInfo, int> result)
        {
            _currentResult = result;
        }
    }
}