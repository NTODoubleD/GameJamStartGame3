using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.DayCycle
{
    public class NewDayDisplayer : MonoBehaviour
    {
        [SerializeField] private DayCycleController _dayCycleController;
        [SerializeField] private TMP_Text _text;

        private readonly TranslatedText _dateText = new TranslatedText("Δενό", "Day");

        public void DisplayNewDay()
        {
            int dayToDisplay = _dayCycleController.CurrentDay + 1;
            _text.text = $"{_dateText.GetText()} {dayToDisplay}";
        }
    }
}