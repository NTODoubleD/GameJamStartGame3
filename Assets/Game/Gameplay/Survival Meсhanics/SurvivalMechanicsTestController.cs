using Game.Gameplay.Crafting;
using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMechanics.Frost;
using Game.Gameplay.SurvivalMeсhanics.Frostbite;
using Game.Gameplay.SurvivalMeсhanics.Hunger;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Survival_Meсhanics
{
    public class SurvivalMechanicsTestController : SerializedMonoBehaviour
    {
        [ShowInInspector] private float _health => _playerMetricsModel.Health;
        [ShowInInspector] private float _heatResistance => _playerMetricsModel.HeatResistance;
        [ShowInInspector] private float _hunger => _playerMetricsModel.Hunger;
        [ShowInInspector] private float _endurance => _playerMetricsModel.Endurance;
        [ShowInInspector] private float _hungerMult => _hungerModel.ConsumptionMultiplyer;

        private PlayerMetricsModel _playerMetricsModel;
        private FrostController _frostController;
        private FrostbiteController _frostbiteController;
        private CookingController _cookingController;
        private HungerModel _hungerModel;
            
        [Inject]
        private void Construct(PlayerMetricsModel playerMetricsModel, FrostController frostController,
            FrostbiteController frostbiteController, CookingController cookingController, HungerModel hungerModel)
        {
            _playerMetricsModel = playerMetricsModel;
            _frostController = frostController;
            _frostbiteController = frostbiteController;
            _cookingController = cookingController;
            _hungerModel = hungerModel;
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
        
        [PropertySpace]
        
        [Button]
        private void StartCooking()
        {
            _cookingController.AddFuelItem();
        }
    }
}