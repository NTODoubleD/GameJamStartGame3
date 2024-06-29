using Game.Gameplay.Interaction;
using Game.UI.Pages;

namespace Game.Gameplay
{
    public class SleighInteractive : InteractRadialMenuObject
    {
        public override void Interact()
        {
            UIManager.OpenPage<RadialMenuPage, RadialMenuArgument>(GetRadialMenuArgument());
        }
    }
}