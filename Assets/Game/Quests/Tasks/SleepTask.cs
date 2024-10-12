using Game.Gameplay.DayCycle;
using Game.Quests.Base;
using UnityEngine;

namespace Game.Quests
{
    public class SleepTask : YakutSubTask
    {
        [SerializeField] private DayCycleController _cycleController;

        public override void Play()
        {
            _cycleController.DayStarted += OnDayStarted;
        }

        public override void Close()
        {
            _cycleController.DayStarted -= OnDayStarted;
        }

        private void OnDayStarted()
        {
            Progress = 1;
        }
    }
}