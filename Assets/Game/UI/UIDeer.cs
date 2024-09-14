using System;
using Game.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIDeer : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private ToggleButton _toggleButton;

        private DeerInfo _deer;

        public event Action Selected;
        public event Action Deselected;

        public bool IsChosen => _toggleButton.State;

        public DeerInfo DeerInfo => _deer;

        public void Initialize(DeerInfo deer)
        {
            _deer = deer;

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