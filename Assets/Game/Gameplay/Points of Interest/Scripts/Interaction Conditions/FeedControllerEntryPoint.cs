using Game.Gameplay.Deers;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class FeedControllerEntryPoint : DeerInteractionCondition
    {
        private DeerFeedController _feedController;

        [Inject]
        private void Init(DeerFeedController deerFeedController)
        {
            _feedController = deerFeedController;
        }

        public override bool ConditionIsDone()
        {
            return _feedController.CanFeed(Deer);
        }

        public void Feed()
        {
            _feedController.Feed(Deer);
        }
    }
}