using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Character;
using Game.Gameplay.SurvivalMechanics;
using Zenject;

namespace Game.Gameplay.SurvivalMeсhanics.Hunger
{
    public class HungerController : IRealtimeSurvivalMechanic, IInitializable
    {
        private readonly HungerConfig _config;
        private readonly CharacterActionsObserver _actionsObserver;
        private readonly PlayerMetricsModel _playerMetricsModel;
        private readonly HungerModel _hungerModel;

        private CancellationTokenSource _cts;
        private float _currentConsumption;
        private bool _isEnabled;

        public HungerController(HungerConfig config, CharacterActionsObserver actionsObserver, 
            PlayerMetricsModel playerMetricsModel, HungerModel hungerModel)
        {
            _config = config;
            _actionsObserver = actionsObserver;
            _playerMetricsModel = playerMetricsModel;
            _hungerModel = hungerModel;
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
            ApplyHungerAsync(_cts.Token).Forget();
        }

        public void Disable()
        {
            if (_isEnabled == false)
                return;
            
            _isEnabled = false;
            _cts.Cancel();
            _cts.Dispose();
        }
        
        private async UniTask ApplyHungerAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _playerMetricsModel.Hunger -= _currentConsumption * _hungerModel.ConsumptionMultiplyer;
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }

        private void OnStartSprinting() => _currentConsumption = _config.GetConsumption(2);

        private void OnEndSprinting() => _currentConsumption = _config.GetConsumption(1);
        
        ~HungerController()
        {
            if (_isEnabled)
                Disable();
            
            _actionsObserver.StartSprinting -= OnStartSprinting;
            _actionsObserver.EndSprinting -= OnEndSprinting;
        }
    }
}