using DoubleDCore.UI.Base;
using UnityEngine;

namespace Game.UI
{
    public class ExitGameButton : ButtonListener
    {
        protected override void OnButtonClicked()
        {
            Application.Quit();
        }
    }
}