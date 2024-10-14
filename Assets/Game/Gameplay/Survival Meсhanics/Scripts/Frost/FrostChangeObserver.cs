using DoubleDCore.UI.Base;
using Game.Gameplay.Crafting;
using Game.UI.Pages;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    public class FrostChangeObserver
    {
        private readonly FrostController _frostController;
        private readonly FrostStarter _frostStarter;
        private readonly CookingController _cookingController;
        private readonly IUIManager _uiManager;

        public FrostChangeObserver(FrostController frostController, FrostStarter frostStarter, CookingController cookingController,
            IUIManager uiManager)
        {
            _frostController = frostController;
            _frostStarter = frostStarter;
            _cookingController = cookingController;
            _uiManager = uiManager;

            _frostController.FrostLevelChanged += OnFrostLevelChanged;
        }

        private void OnFrostLevelChanged(FrostLevel frostLevel)
        {
            if (frostLevel == FrostLevel.Strong)
            {
                _cookingController.StopCookingForced();
                _uiManager.OpenPage<StrongFrostPage, StrongFrostPageArgument>(new StrongFrostPageArgument(_frostStarter));
            }
            else
            {
                _uiManager.ClosePage<StrongFrostPage>();
            }
        }
        
        ~FrostChangeObserver()
        {
            _frostController.FrostLevelChanged -= OnFrostLevelChanged;
        }
    }
}