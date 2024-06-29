using Game.Infrastructure.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.Sleigh
{
    public class SleighSendViewMock : MonoBehaviour, ISleighSendView
    {
        [SerializeField] private ItemInfo[] _infos;

        public event UnityAction<IReadOnlyDictionary<ItemInfo, float>, int> Sended;

        public void Initialize(int deerCapacity, int resourcesCapacity, int currentDeerCount)
        {
            Debug.Log($"INITIALIZED {deerCapacity} {resourcesCapacity} {currentDeerCount}");
        }

        [Button]
        private void SendTest()
        {
            Dictionary<ItemInfo, float> test = new()
            {
                { _infos[0], 0.4f },
                { _infos[1], 0.6f }
            };
            int chosenDeers = 2;
            Debug.Log("TEST");
            Sended?.Invoke(test, chosenDeers);
        }
    }
}