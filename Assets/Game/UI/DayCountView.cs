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
            _text.text = $"{day} день";
        }
    }
}