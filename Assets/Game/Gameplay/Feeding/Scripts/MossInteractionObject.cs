using Game.Gameplay.Interaction;
using UnityEngine;

namespace Game.Gameplay.Feeding
{
    public class MossInteractionObject : SimpleInteractiveObject
    {
        [SerializeField] private PlayerMossPickController _pickController;

        public override void Interact()
        {
            _pickController.InteractWithMossPack();
        }
    }
}