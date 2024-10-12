using DoubleDCore.UI;
using UnityEngine;

namespace Game.Tips.Triggers
{
    public class ButtonPressTrainingTrigger : TrainingTrigger
    {
        [SerializeField] private ClickButton _button;

        private void OnEnable()
        {
            _button.Clicked += OnButtonClicked;
        }

        private void OnDisable()
        {
            _button.Clicked -= OnButtonClicked;
        }

        private void OnButtonClicked()
        {
            StartTraining();
        }
    }
}