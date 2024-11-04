using Game.Gameplay.Deers;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public class PetControllerEntryPoint : DeerInteractionCondition
    {
        private DeerPetController _deerPetController;
        
        [Inject]
        private void Init(DeerPetController deerPetController)
        {
            _deerPetController = deerPetController;
        }
        
        public override bool ConditionIsDone()
        {
            return _deerPetController.CanPet(Deer);
        }

        public void Pet()
        {
            _deerPetController.Pet(Deer);
        }
    }
}