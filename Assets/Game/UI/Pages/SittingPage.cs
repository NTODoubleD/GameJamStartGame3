using DoubleDCore.UI;
using DoubleDCore.UI.Base;

namespace Game.UI.Pages
{
    public class SittingPage : MonoPage, IUIPage
    {
        public void Open()
        {
            SetCanvasState(true);
        }

        public override void Close()
        {
            SetCanvasState(false);
        }
    }
}