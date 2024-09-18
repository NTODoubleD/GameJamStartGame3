using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class EditIconController : MonoBehaviour
    {
        [SerializeField] private Button _editButton;
        [SerializeField] private TMP_InputField _inputField;

        private void OnEnable()
        {
            _editButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _editButton.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _inputField.Select();
        }
    }
}