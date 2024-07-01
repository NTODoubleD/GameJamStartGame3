using Game.Infrastructure.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.Sleigh
{
    public class SleighSendViewMock : MonoBehaviour, ISleighSendView
    {
        [SerializeField] private ItemInfo[] _infos;

        public event UnityAction<IReadOnlyDictionary<ItemInfo, int>, int> Sended;

        public void Initialize(int deerCapacity, int currentDeerCount, IEnumerable<ItemInfo> possibleResources,
            int levelsToDistribute)
        {
            Debug.Log(
                $"INITIALIZED {deerCapacity} {currentDeerCount} {string.Join(", ", possibleResources.Select(x => x.Name))}");
        }

        [Button]
        private void SendTest()
        {
            Dictionary<ItemInfo, int> test = new()
            {
                { _infos[0], 2 },
                { _infos[1], 1 },
                { _infos[2], 1 }
            };
            Sended?.Invoke(test, 0);
        }
    }
}