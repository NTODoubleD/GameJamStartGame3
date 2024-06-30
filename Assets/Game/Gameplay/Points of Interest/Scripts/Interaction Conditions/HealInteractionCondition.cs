using DoubleDTeam.Containers;
using Game.Gameplay.Deers;

namespace Game.Gameplay.Interaction
{
    public class HealInteractionCondition : DeerInteractionCondition
    {
        private DeerHealController _healController;

        private void Awake()
        {
            _healController = Services.SceneContext.GetModule<DeerHealController>();
        }

        public override bool ConditionIsDone()
        {
            return _healController.CanHeal(Deer);
        }
    }
}