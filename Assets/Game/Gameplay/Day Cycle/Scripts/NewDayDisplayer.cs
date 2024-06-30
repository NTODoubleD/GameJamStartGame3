using TMPro;
using UnityEngine;

namespace Game.Gameplay.DayCycle
{
    public class NewDayDisplayer : MonoBehaviour
    {
        [SerializeField] private DayCycleController _dayCycleController;
        [SerializeField] private TMP_Text _text;

        public void DisplayNewDay()
        {
            int dayToDisplay = _dayCycleController.CurrentDay + 1;
            _text.text = $"день {dayToDisplay}";
        }
    }
}