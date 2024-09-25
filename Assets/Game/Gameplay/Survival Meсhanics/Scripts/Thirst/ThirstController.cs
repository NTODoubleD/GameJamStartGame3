using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Character;
using Game.Gameplay.SurvivalMechanics;
using Zenject;

namespace Game.Gameplay.SurvivalMeсhanics.Thirst
{
    public class ThirstController : IRealtimeSurvivalMechanic, IInitializable
    {
        private readonly ThirstConfig _config;
        private readonly CharacterActionsObserver _actionsObserver;
        private readonly PlayerMetricsModel _playerMetricsModel;

        private CancellationTokenSource _cts;
        private float _currentConsumption;
        private bool _isEnabled;

        public ThirstController(ThirstConfig config, CharacterActionsObserver actionsObserver, PlayerMetricsModel playerMetricsModel)
        {
            _config = config;
            _actionsObserver = actionsObserver;
            _playerMetricsModel = playerMetricsModel;
            _currentConsumption = _config.GetConsumption(1);
            
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
            ApplyThirstAsync(_cts.Token).Forget();
        }

        public void Disable()
        {
            if (_isEnabled == false)
                return;
            
            _isEnabled = false;
            _cts.Cancel();
            _cts.Dispose();
        }
        
        private async UniTask ApplyThirstAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _playerMetricsModel.Thirst -= _currentConsumption;
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }

        private void OnStartSprinting() => _currentConsumption = _config.GetConsumption(2);

        private void OnEndSprinting() => _currentConsumption = _config.GetConsumption(1);
        
        ~ThirstController()
        {
            if (_isEnabled)
                Disable();
            
            _actionsObserver.StartSprinting -= OnStartSprinting;
            _actionsObserver.EndSprinting -= OnEndSprinting;
        }
    }
}