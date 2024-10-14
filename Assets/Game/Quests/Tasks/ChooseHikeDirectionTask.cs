using Game.Gameplay.Sleigh;
using Game.Quests.Base;
using Zenject;

namespace Game.Quests
{
    public class ChooseHikeDirectionTask : YakutSubTask
    {
        private SleighSendController _sendController;
        
        [Inject]
        private void Init(SleighSendController sendController)
        {
            _sendController = sendController;
        }
        
        public override void Play()
        {
            _sendController.SleighStarted += OnSleighStarted;
        }

        private void OnSleighStarted(int obj)
        {
            Progress = 1;
        }

        public override void Close()
        {
            _sendController.SleighStarted -= OnSleighStarted;
        }
    }
}