using System;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class BuildingViewUpgrader : MonoBehaviour
    {
        [SerializeField] private BuildingView[] _levelViews;
        [SerializeField] private BuildingConstructionAnimator _contructionAnimator;
        
        public BuildingView CurrentView { get; private set; }

        private void Awake()
        {
            CurrentView = _levelViews[0];
        }

        public void UpgradeTo(int level)
        {
            _contructionAnimator.Animate(() => ChangeBuilding(level));
        }

        public void SetView(int level)
        {
            ChangeBuilding(level);
        }

        private void ChangeBuilding(int level)
        {
            foreach (var levelView in _levelViews)
            {
                levelView.View.SetActive(false);
                levelView.Outline.SetActive(false);
            }

            int index = level - 1;

            if (index >= 0 && index < _levelViews.Length)
            {
                _levelViews[index].View.SetActive(true);
                CurrentView = _levelViews[index];
            }
        }
    }

    [Serializable]
    public class BuildingView
    {
        [SerializeField] private GameObject _view;
        [SerializeField] private GameObject _outline;

        public GameObject View => _view;
        public GameObject Outline => _outline;
    }
}