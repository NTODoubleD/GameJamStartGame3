using DoubleDTeam.Containers;
using Game.Gameplay.Deers;

namespace Game.Gameplay.Interaction
{
    public class KillInteractionCondition : DeerInteractionCondition
    {
        private DeerKillController _killController;

        private void Awake()
        {
            _killController = Services.SceneContext.GetModule<DeerKillController>();
        }

        public override bool ConditionIsDone()
        {
            return _killController.CanKill(Deer);
        }
    }
}