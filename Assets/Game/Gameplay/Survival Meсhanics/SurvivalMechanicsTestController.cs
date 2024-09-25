using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMechanics.Frost;
using Game.Gameplay.SurvivalMeсhanics.Frostbite;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Survival_Meсhanics
{
    public class SurvivalMechanicsTestController : SerializedMonoBehaviour
    {
        [ShowInInspector] private int _health => _playerMetricsModel.Health;
        [ShowInInspector] private int _heatResistance => _playerMetricsModel.HeatResistance;
        [ShowInInspector] private int _hunger => _playerMetricsModel.Hunger;
        [ShowInInspector] private int _thirst => _playerMetricsModel.Thirst;
        [ShowInInspector] private int _endurance => _playerMetricsModel.Endurance;

        private PlayerMetricsModel _playerMetricsModel;
        private FrostController _frostController;
        private FrostbiteController _frostbiteController;
            
        [Inject]
        private void Construct(PlayerMetricsModel playerMetricsModel, FrostController frostController, FrostbiteController frostbiteController)
        {
            _playerMetricsModel = playerMetricsModel;
            _frostController = frostController;
            _frostbiteController = frostbiteController;
        }
        
        [Button]
        private void EnableStrongFrost()
        {
            _frostController.Enable(FrostLevel.Strong);
        }

        [Button]
        private void DisableFrost()
        {
            _frostController.Disable();
        }

        [PropertySpace]

        [Button]
        private void AddHunger(int value) => _playerMetricsModel.Hunger += value;
        
        [Button]
        private void AddHeatResistance(int value) => _playerMetricsModel.HeatResistance += value;
    }
}