using Game.Gameplay.Deers;
using Game.Quests.Base;

namespace Game.Quests
{
    public class FeedDeerTask : YakutSubTask
    {
        public override void Play()
        {
            DeerFeedController.Ate += DeeOnAte;
        }

        public override void Close()
        {
            DeerFeedController.Ate -= DeeOnAte;
        }

        private void DeeOnAte()
        {
            Progress += 1;
        }
    }
}