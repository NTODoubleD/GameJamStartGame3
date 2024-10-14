using System;
using DoubleDCore.Service;
using Zenject;

namespace Game.Gameplay.DayCycle
{
    public class SleepingController : MonoService
    {
        private DayCycleController _dayCycleController;

        public event Action SleepCalled;
        
        [Inject]
        private void Init(DayCycleController dayCycleController)
        {
            _dayCycleController = dayCycleController;
        }

        public void Sleep()
        {
            SleepCalled?.Invoke();
            _dayCycleController.EndDay();
        }
    }
}