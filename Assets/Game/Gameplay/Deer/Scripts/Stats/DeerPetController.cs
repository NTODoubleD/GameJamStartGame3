namespace Game.Gameplay.Deers
{
    public class DeerPetController
    {
        private readonly CharacterAnimatorController _characterAnimatorController;

        public DeerPetController(CharacterAnimatorController characterAnimatorController)
        {
            _characterAnimatorController = characterAnimatorController;
        }

        public bool CanPet(Deer deer)
        {
            return !deer.DeerInfo.IsDead;
        }

        public void Pet(Deer deer)
        {
            if (CanPet(deer) == false)
                return;
            
            if (deer.DeerInfo.Age == DeerAge.Young)
                _characterAnimatorController.AnimateMiniPetting(deer.EnterWalkingState);
            else
                _characterAnimatorController.AnimateBigPetting(deer.EnterWalkingState);
            
            deer.EnterPetState();
        }
    }
}