using DoubleDTeam.Containers;
using Game.Gameplay.Deers;

namespace Game.Gameplay.Interaction
{
    public class CutInteractionCondition : DeerInteractionCondition
    {
        private DeerCutController _cutController;

        private void Awake()
        {
            _cutController = Services.SceneContext.GetModule<DeerCutController>();
        }

        public override bool ConditionIsDone()
        {
            return _cutController.CanCut(Deer);
        }
    }
}