using Game.Gameplay.DayCycle;
using Game.Gameplay.SurvivalMechanics;

namespace Game.Gameplay.SurvivalMeсhanics.Health
{
    public class HealthRestoreController
    {
        private readonly SleepingController _sleepingController;
        private readonly PlayerMetricsModel _model;

        public HealthRestoreController(SleepingController sleepingController, PlayerMetricsModel model)
        {
            _sleepingController = sleepingController;
            _model = model;
            
            _sleepingController.SleepCalled += RestoreHealth;
        }

        private void RestoreHealth()
        {
            _model.Health = _model.MaxValue;
        }
        
        ~HealthRestoreController()
        {
            _sleepingController.SleepCalled -= RestoreHealth;
        }
    }
}