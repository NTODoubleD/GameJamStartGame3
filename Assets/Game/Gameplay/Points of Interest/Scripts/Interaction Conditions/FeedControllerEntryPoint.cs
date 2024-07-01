using DoubleDTeam.Containers;
using Game.Gameplay.Deers;

namespace Game.Gameplay.Interaction
{
    public class FeedControllerEntryPoint : DeerInteractionCondition
    {
        private DeerFeedController _feedController;

        private void Awake()
        {
            _feedController = Services.SceneContext.GetModule<DeerFeedController>();
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