using System;
using DoubleDCore.UI.Base;
using Game.UI.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIWikiMenuItem : ButtonListener
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _highlightImage;

        public event Action<UIWikiMenuItem> Clicked;

        private TrainingInfo _trainingInfo;

        public TrainingInfo TrainingInfo => _trainingInfo;

        public void Initialize(TrainingInfo trainingInfo)
        {
            _trainingInfo = trainingInfo;
            _text.text = trainingInfo.Name;
        }

        public void SetHighlight(bool isActive)
        {
            _highlightImage.enabled = isActive;
        }

        protected override void OnButtonClicked()
        {
            Clicked?.Invoke(this);
        }
    }
}