using Game.Gameplay.Deers;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class KillControllerEntryPoint : DeerInteractionCondition
    {
        private DeerKillController _killController;

        [Inject]
        private void Init(DeerKillController deerKillController)
        {
            _killController = deerKillController;
        }

        public override bool ConditionIsDone()
        {
            return _killController.CanKill(Deer);
        }

        public void Kill()
        {
            _killController.Kill(Deer);
        }
    }
}