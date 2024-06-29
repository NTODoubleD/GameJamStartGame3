using Game.Gameplay.Buildings;
using Game.Gameplay.DayCycle;
using Game.Infrastructure.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Sleigh
{
    public class SleighSendController : MonoBehaviour
    {
        [SerializeField] private SleighSendViewMock _sendView;
        [SerializeField] private SleighBuilding _sleigh;
        [SerializeField] private PastureBuilding _pasture;
        [SerializeField] private SleighReceiveController _receiveController;
        [SerializeField] private DayCycleController _dayCycleController;

        private void Awake()
        {
            _sendView.Initialize(_sleigh.DeerCapacity, _sleigh.ItemCapacity, _pasture.DeerCount);
        }

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

        private void OnSleighUpgraded()
        {
            _sendView.Initialize(_sleigh.DeerCapacity, _sleigh.ItemCapacity, _pasture.DeerCount);
        }

        private void Send(IReadOnlyDictionary<ItemInfo, float> itemPercentages, int deerCount)
        {
            int totalCapacity = (int)(_sleigh.ItemCapacity * (deerCount/(float)_sleigh.DeerCapacity));
            Dictionary<ItemInfo, int> resultItemsCount = new Dictionary<ItemInfo, int>();

            foreach (var keyPair in itemPercentages)
                resultItemsCount.Add(keyPair.Key, (int)(keyPair.Value * totalCapacity));

            _receiveController.SetReceiveInfo(resultItemsCount);
            _dayCycleController.EndDay();
        }
    }
}