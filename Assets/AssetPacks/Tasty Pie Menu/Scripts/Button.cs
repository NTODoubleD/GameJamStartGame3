﻿using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Xamin
{
    [RequireComponent(typeof(Image))]
    public class Button : MonoBehaviour
    {
        [Tooltip("Your actions, that will be executed when the buttons is pressed")]
        public UnityEvent action; 
        [Tooltip("The icon of this button")]
        public Sprite image;
        [Tooltip("If this button can be pressed or not. False = grayed out button")]
        public bool unlocked;
        [Tooltip("Can be used to reference the button via code.")]
        public string id;

        private TMP_Text _text;
        
        public Color customColor;
        public bool useCustomColor;

        [FormerlySerializedAs("_optionsName")] public TextMeshProUGUI optionsName;

        private UnityEngine.UI.Image imageComponent;
        private bool _isimageComponentNotNull;
        private bool _isTextComponentNotNull;

        void Start()
        {
            imageComponent = GetComponent<UnityEngine.UI.Image>();
            _text = GetComponentInChildren<TMP_Text>();
            if (image)
                imageComponent.sprite = image;
            _isimageComponentNotNull = imageComponent != null; // This check avoids expensive not null comparisons at runtime.
            _isTextComponentNotNull = _text != null;
        }

        public Color currentColor
        {
            get { return imageComponent.color; }
        }

        public void SetColor(Color c)
        {
            if (_isimageComponentNotNull)
                imageComponent.color = c;
            if (_isTextComponentNotNull)
                _text.color = c;
        }
        
        /// <summary>
        /// This method is responsible for handling the UnityEvent execution 
        /// </summary>
        public void ExecuteAction()
        {
            action.Invoke();
        }
    }
}