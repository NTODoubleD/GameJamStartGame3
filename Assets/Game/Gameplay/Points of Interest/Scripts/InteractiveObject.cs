using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        [SerializeField] private GameObject _outlineCopy;

        public void EnableHighlight()
        {
            _outlineCopy.SetActive(true);
        }

        public void DisableHighlight()
        {
            _outlineCopy.SetActive(false);
        }

        public abstract void Interact();
    }
}