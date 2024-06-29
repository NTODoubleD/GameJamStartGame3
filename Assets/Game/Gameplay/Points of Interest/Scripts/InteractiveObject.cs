using Game.Gameplay.Buildings;
using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        [SerializeField] private BuildingViewUpgrader _viewUpgrader;

        public void EnableHighlight()
        {
            _viewUpgrader.CurrentView.Outline.SetActive(true);
        }

        public void DisableHighlight()
        {
            _viewUpgrader.CurrentView.Outline.SetActive(false);
        }

        public abstract void Interact();
    }
}