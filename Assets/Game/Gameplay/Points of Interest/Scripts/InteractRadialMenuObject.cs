using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.UI.Pages;
using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public abstract class InteractRadialMenuObject : InteractiveObject
    {
        [SerializeField] protected string _name;
        [SerializeField] protected List<RadialButtonInfo> _operations;

        protected IUIManager UIManager;

        protected RadialMenuArgument GetRadialMenuArgument()
        {
            return new RadialMenuArgument
            {
                Name = _name,
                Buttons = _operations
            };
        }

        protected virtual void Awake()
        {
            UIManager = Services.ProjectContext.GetModule<IUIManager>();
        }

        public abstract override void Interact();
    }
}