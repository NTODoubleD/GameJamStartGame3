using Game.Infrastructure.Items;
using System.Collections.Generic;
using Game.Gameplay.Items;
using UnityEngine.Events;

namespace Game.Gameplay.Sleigh
{
    public interface ISleighSendView
    {
        void Initialize(int deerCapacity, int currentDeerCount, IEnumerable<GameItemInfo> possibleResources, int levelsToDistribute);
        event UnityAction<IReadOnlyDictionary<GameItemInfo, int>, int> Sended;
    }
}