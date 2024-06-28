using System;
using DoubleDTeam.UI.Base;

namespace DoubleDTeam.UI
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