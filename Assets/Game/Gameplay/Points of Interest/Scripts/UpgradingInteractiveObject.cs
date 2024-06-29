using Game.Gameplay.Buildings;
using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public abstract class UpgradingInteractiveObject : InteractiveObject
    {
        [SerializeField] private BuildingViewUpgrader _viewUpgrader;

        public override void EnableHighlight()
        {
            _viewUpgrader.CurrentView.Outline.SetActive(true);
        }

        public override void DisableHighlight()
        {
            _viewUpgrader.CurrentView.Outline.SetActive(false);
        }
    }
}