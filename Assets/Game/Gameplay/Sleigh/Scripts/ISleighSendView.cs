using Game.Infrastructure.Items;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Game.Gameplay.Sleigh
{
    public interface ISleighSendView
    {
        void Initialize(int deerCapacity, int resourcesCapacity, int currentDeerCount);
        event UnityAction<IReadOnlyDictionary<ItemInfo, float>, int> Sended;
    }
}