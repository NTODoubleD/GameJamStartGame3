using Game.Gameplay.Buildings;
using Game.Infrastructure.Items;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Sleigh
{
    public class SleighSendController : MonoBehaviour
    {
        [OdinSerialize] private ISleighSendView _sendView;
        [SerializeField] private SleighBuilding _sleigh;
        [SerializeField] private SleighReceiveController _receiveController;

        private void Awake()
        {
            _sendView.Initialize(_sleigh.DeerCapacity, _sleigh.ItemCapacity);
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
            _sendView.Initialize(_sleigh.DeerCapacity, _sleigh.ItemCapacity);
        }

        private void Send(IReadOnlyDictionary<ItemInfo, float> itemPercentages, int deerCount)
        {
            int totalCapacity = (int)(_sleigh.ItemCapacity * (deerCount/(float)_sleigh.DeerCapacity));
            Dictionary<ItemInfo, int> resultItemsCount = new Dictionary<ItemInfo, int>();

            foreach (var keyPair in itemPercentages)
                resultItemsCount.Add(keyPair.Key, (int)(keyPair.Value * totalCapacity));

            _receiveController.SetReceiveInfo(resultItemsCount);
        }
    }
}