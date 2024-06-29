﻿using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem.Base;
using DoubleDTeam.UI;
using UnityEngine.EventSystems;

namespace Game.InputMaps
{
    public class UIInputMap : InputMap
    {
        private EventSystem _eventSystem;

        public override void Initialize()
        {
            _eventSystem = Services.ProjectContext.GetModule<EventSystemProvider>().EventSystem;
        }

        public override void Enable()
        {
            base.Enable();

            _eventSystem.enabled = true;
        }

        public override void Disable()
        {
            base.Disable();

            _eventSystem.enabled = false;
        }
    }
}