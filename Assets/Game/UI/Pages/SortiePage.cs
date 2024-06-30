using System.Collections.Generic;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using Game.Gameplay.Sleigh;
using Game.Infrastructure.Items;
using UnityEngine.Events;

namespace Game.UI.Pages
{
    public class SortiePage : MonoPage, IUIPage, ISleighSendView
    {
        public void Open()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(int deerCapacity, int currentDeerCount, IEnumerable<ItemInfo> possibleResources, int levelsToDistribute)
        {
            throw new System.NotImplementedException();
        }

        public event UnityAction<IReadOnlyDictionary<ItemInfo, int>> Sended;
    }
}