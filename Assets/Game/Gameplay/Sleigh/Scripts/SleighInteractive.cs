using Game.Gameplay.Interaction;
using Game.UI.Pages;

namespace Game.Gameplay
{
    public class SleighInteractive : UpgradingInteractiveObject
    {
        public override void Interact()
        {
            UIManager.OpenPage<RadialMenuPage, RadialMenuArgument>(GetRadialMenuArgument());
        }

        public void OpenSortiePage()
        {
            UIManager.OpenPage<SortiePage>();
        }
    }
}