using Game.Gameplay.DayCycle;
using Game.Gameplay.SurvivalMechanics;

namespace Game.Gameplay.SurvivalMeсhanics.Endurance
{
    public class EnduranceRestoreController
    {
        private readonly EnduranceConfig _enduranceConfig;
        private readonly SleepingController _sleepingController;
        private readonly PlayerMetricsModel _playerMetricsModel;

        public EnduranceRestoreController(EnduranceConfig enduranceConfig, SleepingController sleepingController,
            PlayerMetricsModel playerMetricsModel)
        {
            _enduranceConfig = enduranceConfig;
            _sleepingController = sleepingController;
            _playerMetricsModel = playerMetricsModel;

            _sleepingController.SleepCalled += OnSleepCalled;
        }

        private void OnSleepCalled()
        {
            _playerMetricsModel.Endurance = _enduranceConfig.RestoreDayValue;
        }

        ~EnduranceRestoreController()
        {
            _sleepingController.SleepCalled -= OnSleepCalled;
        }
    }
}