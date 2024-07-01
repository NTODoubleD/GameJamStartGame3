using Game.Infrastructure.Items;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Game.Gameplay.Sleigh
{
    public interface ISleighSendView
    {
        void Initialize(int deerCapacity, int currentDeerCount, IEnumerable<ItemInfo> possibleResources, int levelsToDistribute);
        event UnityAction<IReadOnlyDictionary<ItemInfo, int>, int> Sended;
    }
}