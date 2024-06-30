using DoubleDTeam.Containers;
using DoubleDTeam.UI.Base;
using Game.UI.Pages;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        [SerializeField] protected string _name;
        [SerializeField] protected List<RadialButtonInfo> _operations;

        protected IUIManager UIManager;

        protected virtual RadialMenuArgument GetRadialMenuArgument()
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

        public abstract void EnableHighlight();
        public abstract void DisableHighlight();
        public abstract void Interact();
    }
}