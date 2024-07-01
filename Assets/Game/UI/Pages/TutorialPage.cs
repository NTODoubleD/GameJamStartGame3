﻿using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;

namespace Game.UI.Pages
{
    public class TutorialPage : MonoPage, IUIPage
    {
        private void Awake()
        {
            Close();
        }

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