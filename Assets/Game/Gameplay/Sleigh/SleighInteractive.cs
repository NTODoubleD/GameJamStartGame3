using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.Gameplay.Interaction;
using Game.UI;
using UnityEngine;

namespace Game.Gameplay
{
    public class SleighInteractive : InteractiveObject
    {
        [SerializeField] private string _name;
        [SerializeField] private List<RadialButtonInfo> _operations;

        private IUIManager _uiManager;
        private RadialMenuArgument _radialMenuArgument;

        protected void Awake()
        {
            _uiManager = Services.ProjectContext.GetModule<IUIManager>();

            _radialMenuArgument = new RadialMenuArgument
            {
                Name = _name,
                Buttons = _operations
            };
        }

        public override void Interact()
        {
            _uiManager.OpenPage<RadialMenuPage, RadialMenuArgument>(_radialMenuArgument);
        }
    }
}