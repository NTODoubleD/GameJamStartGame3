﻿using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMechanics.Frost;
using Game.Gameplay.SurvivalMeсhanics.Frostbite;
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
        [ShowInInspector] private float _thirst => _playerMetricsModel.Thirst;
        [ShowInInspector] private float _endurance => _playerMetricsModel.Endurance;

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