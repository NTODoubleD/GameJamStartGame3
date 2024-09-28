using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Character;
using Game.Gameplay.DayCycle;
using Game.Gameplay.SurvivalMechanics;
using Zenject;

namespace Game.Gameplay.SurvivalMeсhanics.Endurance
{
    public class EnduranceController : IRealtimeSurvivalMechanic, IInitializable
    {
        private readonly EnduranceConfig _config;
        private readonly CharacterActionsObserver _actionsObserver;
        private readonly PlayerMetricsModel _playerMetricsModel;

        private CancellationTokenSource _cts;
        private float _currentConsumption;
        private bool _isEnabled;

        public EnduranceController(EnduranceConfig config, CharacterActionsObserver actionsObserver,
            PlayerMetricsModel playerMetricsModel)
        {
            _config = config;
            _actionsObserver = actionsObserver;
            _playerMetricsModel = playerMetricsModel;
            _currentConsumption = _config.GetConsumption(ActionType.Constant);
            
            _actionsObserver.StartSprinting += OnStartSprinting;
            _actionsObserver.EndSprinting += OnEndSprinting;
        }

        public void Initialize()
        {
            Enable();
        }

        public void Enable()
        {
            if (_isEnabled)
                Disable();
            
            _isEnabled = true;
            _cts = new CancellationTokenSource();
            ApplyEnduranceAsync(_cts.Token).Forget();
        }

        public void Disable()
        {
            if (_isEnabled == false)
                return;
            
            _isEnabled = false;
            _cts.Cancel();
            _cts.Dispose();
        }
        
        private async UniTask ApplyEnduranceAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _playerMetricsModel.Endurance -= _currentConsumption;
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }

        private void OnStartSprinting() => _currentConsumption = _config.GetConsumption(ActionType.Sprint);

        private void OnEndSprinting() => _currentConsumption = _config.GetConsumption(ActionType.Constant);
        
        ~EnduranceController()
        {
            if (_isEnabled)
                Disable();
            
            _actionsObserver.StartSprinting -= OnStartSprinting;
            _actionsObserver.EndSprinting -= OnEndSprinting;
        }
    }
}