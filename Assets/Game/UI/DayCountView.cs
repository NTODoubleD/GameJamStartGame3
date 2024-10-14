using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using Game.Gameplay.DayCycle;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class DayCountView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private DayCycleController _dayCycleController;

        private readonly TranslatedText _translatedText = new("День {0}", "Day {0}");

        [Inject]
        private void Init(DayCycleController dayCycleController)
        {
            _dayCycleController = dayCycleController;
            _dayCycleController.DayStarted += OnDayStarted;
        }

        private void Awake()
        {
            OnDayStarted();
        }

        private void OnDestroy()
        {
            _dayCycleController.DayStarted -= OnDayStarted;
        }

        private void OnDayStarted()
        {
            Refresh(_dayCycleController.CurrentDay);
        }

        private void Refresh(int day)
        {
            _text.text = string.Format(_translatedText.GetText(), day);
        }
    }
}