using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public abstract class MultipleInteractiveObject : InteractiveObject
    {
        [SerializeField] private GameObject[] _outlines;

        public override void EnableHighlight()
        {
            foreach (var outline in _outlines)
                outline.SetActive(true);
        }
        
        public override void DisableHighlight()
        {
            foreach (var outline in _outlines)
                outline.SetActive(false);
        }
    }
}