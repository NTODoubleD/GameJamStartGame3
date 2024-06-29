using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.Gameplay.Interaction;
using Game.UI;
using UnityEngine;

namespace Game.Gameplay
{
    public class Sleigh : InteractiveObject
    {
        [SerializeField] private string _name;

        private IUIManager _uiManager;
        private RadialMenuArgument _radialMenuArgument;

        protected void Awake()
        {
            _uiManager = Services.ProjectContext.GetModule<IUIManager>();

            _radialMenuArgument = new RadialMenuArgument
            {
                Name = _name,
            };
        }

        public override void Interact()
        {
            _uiManager.OpenPage<RadialMenuPage, RadialMenuArgument>(_radialMenuArgument);
        }
    }
}