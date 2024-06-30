using Game.Gameplay.Interaction;
using Game.UI.Pages;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay
{
    public class DeerInteractive : SimpleInteractiveObject
    {
        [SerializeField] private Deer _deer;

        public UnityEvent Interacted;

        protected override RadialMenuArgument GetRadialMenuArgument()
        {
            return new RadialMenuArgument()
            {
                Name = _name + "\n" + _deer.DeerInfo.Name,
                Buttons = _operations
            };
        }

        public override void Interact()
        {
            UIManager.OpenPage<RadialMenuPage, RadialMenuArgument>(GetRadialMenuArgument());
            Interacted?.Invoke();
        }
    }
}