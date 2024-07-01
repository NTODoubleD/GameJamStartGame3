using DoubleDTeam.Containers;
using Game.Gameplay.Deers;

namespace Game.Gameplay.Interaction
{
    public class CutControllerEntryPoint : DeerInteractionCondition
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

        public void Cut()
        {
            _cutController.Cut(Deer);
        }
    }
}