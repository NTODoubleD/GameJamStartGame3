using Game.UI.Pages;
using System.Collections.Generic;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Interaction
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        [SerializeField] protected string _name;
        [SerializeField] private float _distanceToInteract = 1.5f;
        [SerializeField] protected List<RadialButtonInfo> _operations;

        protected IUIManager UIManager;

        public float DistanceToInteract => _distanceToInteract;

        protected virtual RadialMenuArgument GetRadialMenuArgument()
        {
            return new RadialMenuArgument
            {
                Name = _name,
                Buttons = _operations
            };
        }

        [Inject]
        private void Init(IUIManager uiManager)
        {
            UIManager = uiManager;
        }

        public abstract void EnableHighlight();
        public abstract void DisableHighlight();
        public abstract void Interact();
    }
}