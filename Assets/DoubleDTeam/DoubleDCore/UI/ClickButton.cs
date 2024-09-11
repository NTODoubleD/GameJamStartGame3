using System;
using DoubleDCore.UI.Base;

namespace DoubleDCore.UI
{
    public class ClickButton : ButtonListener
    {
        public event Action Clicked;

        protected override void OnButtonClicked()
        {
            Clicked?.Invoke();
        }
    }
}