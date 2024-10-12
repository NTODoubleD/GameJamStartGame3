using DoubleDCore.QuestsSystem.Data;
using Game.Gameplay.Items;
using Game.Gameplay.Survival_Meсhanics.Scripts.Common;
using Game.Quests.Base;
using UnityEngine;

namespace Game.Quests.Tasks
{
    public class ConsumeItemTask : YakutSubTask, IGameItemUseObserver
    {
        [SerializeField] private GameItemInfo _itemInfo;
        
        public override void Play()
        {
            
        }

        public override void Close()
        {
            
        }

        public void OnItemUsed(GameItemInfo itemInfo)
        {
            if (Status != QuestStatus.InProgress)
                return;

            if (itemInfo == _itemInfo)
                Progress += 1;
        }
    }
}