using Game.Gameplay.Deers;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class HealControllerEntryPoint : DeerInteractionCondition
    {
        private DeerHealController _healController;

        [Inject]
        private void Init(DeerHealController deerHealController)
        {
            _healController = deerHealController;
        }

        public override bool ConditionIsDone()
        {
            return _healController.CanHeal(Deer);
        }

        public void Heal()
        {
            _healController.Heal(Deer);
        }
    }
}