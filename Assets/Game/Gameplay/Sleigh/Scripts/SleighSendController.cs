using System;
using Game.Gameplay.Buildings;
using Game.Gameplay.DayCycle;
using Game.Infrastructure.Items;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.UI.Pages;
using UnityEngine;

namespace Game.Gameplay.Sleigh
{
    public class SleighSendController : MonoBehaviour
    {
        [SerializeField] private SortiePage _sendView;
        [SerializeField] private SleighBuilding _sleigh;
        [SerializeField] private PastureBuilding _pasture;
        [SerializeField] private SleighReceiveController _receiveController;
        [SerializeField] private DayCycleController _dayCycleController;
        [SerializeField] private ItemInfo[] _possibleResources;
        [SerializeField] private int _levelsToDistribute = 4;

        private async void Start()
        {
            await UniTask.NextFrame();

            _sendView.Initialize(_sleigh.DeerCapacity, _pasture.DeerCount, _possibleResources, _levelsToDistribute);
        }

        public event Action<int> SleighStarted;

        private void OnEnable()
        {
            _sleigh.Upgraded += OnSleighUpgraded;
            _sendView.Sended += Send;
        }

        private void OnDisable()
        {
            _sleigh.Upgraded -= OnSleighUpgraded;
            _sendView.Sended -= Send;
        }

        public int GetResourcesLimitLevel(ItemInfo resource, int chosenDeerCount)
        {
            float partOfTarget = (float)chosenDeerCount / _sleigh.DeerCapacity;
            float partToFeel = (1 - partOfTarget) * 100;
            int maxLevel = _sleigh.GetItemLevelCounts(resource).Length;

            if (partToFeel <= 25)
                return maxLevel - 1;
            else if (partToFeel > 25)
                return maxLevel - 2;

            return maxLevel;
        }

        private void OnSleighUpgraded()
        {
            _sendView.Initialize(_sleigh.DeerCapacity, _pasture.DeerCount, _possibleResources, _levelsToDistribute);
        }

        private void Send(IReadOnlyDictionary<ItemInfo, int> itemLevels, int amountDeer)
        {
            Dictionary<ItemInfo, int> resultItemsCount = new Dictionary<ItemInfo, int>();

            foreach (var keyPair in itemLevels)
            {
                if (keyPair.Value == 0)
                {
                    resultItemsCount.Add(keyPair.Key, 0);
                    continue;
                }

                int[] counts = _sleigh.GetItemLevelCounts(keyPair.Key);
                resultItemsCount.Add(keyPair.Key, counts[keyPair.Value - 1]);
            }

            _receiveController.SetReceiveInfo(resultItemsCount);
            _dayCycleController.EndDay();

            SleighStarted?.Invoke(amountDeer);
        }
    }
}