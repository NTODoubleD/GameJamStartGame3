using Game.Gameplay.Crafting;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    public class FrostChangeObserver
    {
        private readonly FrostController _frostController;
        private readonly CookingController _cookingController;

        public FrostChangeObserver(FrostController frostController, CookingController cookingController)
        {
            _frostController = frostController;
            _cookingController = cookingController;

            _frostController.FrostLevelChanged += OnFrostLevelChanged;
        }

        private void OnFrostLevelChanged(FrostLevel frostLevel)
        {
            if (frostLevel == FrostLevel.Strong)
                _cookingController.StopCookingForced();
        }
        
        ~FrostChangeObserver()
        {
            _frostController.FrostLevelChanged -= OnFrostLevelChanged;
        }
    }
}