using DoubleDCore.UI.Base;
using Game.Feedbacks;
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
        private readonly StormFeedback _stormFeedback;

        public FrostChangeObserver(FrostController frostController, FrostStarter frostStarter, CookingController cookingController,
            IUIManager uiManager, StormFeedback stormFeedback)
        {
            _frostController = frostController;
            _frostStarter = frostStarter;
            _cookingController = cookingController;
            _uiManager = uiManager;
            _stormFeedback = stormFeedback;

            _frostController.FrostLevelChanged += OnFrostLevelChanged;
        }

        private void OnFrostLevelChanged(FrostLevel frostLevel)
        {
            if (frostLevel == FrostLevel.Strong)
            {
                _cookingController.StopCookingForced();
                _uiManager.OpenPage<StrongFrostPage, StrongFrostPageArgument>(new StrongFrostPageArgument(_frostStarter));
                _stormFeedback.StartAnimation();
            }
            else
            {
                _uiManager.ClosePage<StrongFrostPage>();
                _stormFeedback.StopAnimation();
            }
        }
        
        ~FrostChangeObserver()
        {
            _frostController.FrostLevelChanged -= OnFrostLevelChanged;
        }
    }
}