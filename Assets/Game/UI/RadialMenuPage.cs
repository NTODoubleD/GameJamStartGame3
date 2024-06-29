using System;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using UnityEngine;

namespace Game.UI
{
    public class RadialMenuPage : MonoPage, IPayloadPage<RadialMenuArgument>
    {
        private RadialMenuArgument _radialMenuArgument;

        public event Action ChoiceIsMade;

        public void Open(RadialMenuArgument context)
        {
            _radialMenuArgument = context;

            SetCanvasState(true);
        }

        public override void Close()
        {
            _radialMenuArgument = null;

            SetCanvasState(false);
        }
    }

    public class RadialMenuArgument
    {
        public string Name;
    }
}