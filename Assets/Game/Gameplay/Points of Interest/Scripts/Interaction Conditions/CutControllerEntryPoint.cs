using Game.Gameplay.Deers;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class CutControllerEntryPoint : DeerInteractionCondition
    {
        private DeerCutController _cutController;

        [Inject]
        private void Init(DeerCutController deerCutController)
        {
            _cutController = deerCutController;
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