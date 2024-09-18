using System;
using Game.Gameplay;
using Game.Gameplay.Scripts.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class UIDeer : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _frame;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private ToggleButton _toggleButton;

        private DeerInfo _deer;
        private DeerImagesConfig _deerImagesConfig;

        public event Action Selected;
        public event Action Deselected;

        public bool IsChosen => _toggleButton.State;

        public DeerInfo DeerInfo => _deer;

        public void Initialize(DeerInfo deer, DeerImagesConfig imagesConfig)
        {
            _deerImagesConfig = imagesConfig;
            _deer = deer;
            
            _image.sprite = _deerImagesConfig.GetDeerImage(deer.Age, deer.Gender);
            _frame.color = _deer.Gender == GenderType.Female
                ? _deerImagesConfig.FemaleFrameColor
                : _deerImagesConfig.MaleFrameColor;
            
            _nameText.text = DeerInfo.Name;
            _toggleButton.Initialize(false, () => Selected?.Invoke(), () => Deselected?.Invoke());
        }

        public void SetActiveToggleButton(bool isActive)
        {
            _toggleButton.SetActiveButton(isActive);
        }

        public void Reset()
        {
            _toggleButton.SetState(false);
        }
    }
}