using Game.Gameplay.Items;
using Game.Gameplay.Survival_Meсhanics.Scripts.Common;
using Game.Quests.Base;
using UnityEngine;

namespace Game.Quests.Tasks
{
    public class ConsumeItemTask : YakutSubTask, IGameItemUseObserver
    {
        [SerializeField] private GameItemInfo _itemInfo;

        private bool _isActive;
        
        public override void Play()
        {
            _isActive = true;
        }

        public override void Close()
        {
            _isActive = false;
        }

        public void OnItemUsed(GameItemInfo itemInfo)
        {
            if (_isActive == false)
                return;

            if (itemInfo == _itemInfo)
                Progress += 1;
        }
    }
}