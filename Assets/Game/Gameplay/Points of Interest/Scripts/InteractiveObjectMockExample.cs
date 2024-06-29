using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public class InteractiveObjectMockExample : InteractiveObject
    {
        public override void Interact()
        {
            Debug.Log("INTERACTED");
        }
    }
}