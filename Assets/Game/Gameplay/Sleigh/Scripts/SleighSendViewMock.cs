using Game.Infrastructure.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.Sleigh
{
    public class SleighSendViewMock : MonoBehaviour, ISleighSendView
    {
        [SerializeField] private GameItemInfo[] _infos;

        public event UnityAction<IReadOnlyDictionary<GameItemInfo, int>, int> Sended;

        public void Initialize(int deerCapacity, int currentDeerCount, IEnumerable<GameItemInfo> possibleResources,
            int levelsToDistribute)
        {
            Debug.Log(
                $"INITIALIZED {deerCapacity} {currentDeerCount} {string.Join(", ", possibleResources.Select(x => x.Name))}");
        }

        [Button]
        private void SendTest()
        {
            Dictionary<GameItemInfo, int> test = new()
            {
                { _infos[0], 2 },
                { _infos[1], 1 },
                { _infos[2], 1 }
            };
            Sended?.Invoke(test, 0);
        }
    }
}