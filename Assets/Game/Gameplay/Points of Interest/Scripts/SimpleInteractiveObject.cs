using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public abstract class SimpleInteractiveObject : InteractiveObject
    {
        [SerializeField] private GameObject _outline;

        public override void EnableHighlight()
        {
            _outline.SetActive(true);
        }

        public override void DisableHighlight()
        {
            _outline.SetActive(false);
        }
    }
}