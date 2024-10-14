using DoubleDCore.UI.Base;
using Game.UI.Pages;

namespace Game.UI
{
    public class TooltipController
    {
        private readonly IUIManager _uiManager;

        public TooltipController(IUIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        public void EnableTooltip(string text)
        {
            _uiManager.OpenPage<TooltipPage, TooltipArgument>(new TooltipArgument(text));
        }

        public void DisableTooltip()
        {
            _uiManager.ClosePage<TooltipPage>();
        }
    }
}