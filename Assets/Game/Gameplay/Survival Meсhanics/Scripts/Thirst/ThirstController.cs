using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Items;
using Game.Gameplay.Survival_Meсhanics.Scripts.Common;
using Game.Gameplay.SurvivalMeсhanics.Exhaustion;
using Game.Gameplay.SurvivalMeсhanics.Hunger;
using Game.Infrastructure.Storage;
using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Thirst
{
    public class ThirstController : IGameItemUseObserver
    {
        private readonly ThirstConfig _config;
        private readonly HungerModel _model;
        private readonly ItemStorage _itemStorage;
        private readonly ExhaustionController _exhaustionController;

        private readonly HashSet<GameItemInfo> _waterResources = new();

        private float _effectTimeLeft;

        public ThirstController(ThirstConfig config, HungerModel model,
            ItemStorage itemStorage, ExhaustionController exhaustionController)
        {
            _config = config;
            _model = model;
            _itemStorage = itemStorage;
            _exhaustionController = exhaustionController;

            foreach (var waterResource in _config.WaterResources)
                _waterResources.Add(waterResource);
        }
        
        public void OnItemUsed(GameItemInfo itemInfo)
        {
            if (_waterResources.Contains(itemInfo) == false || _itemStorage.GetCount(itemInfo) == 0)
                return;

            if (_effectTimeLeft <= 0)
            {
                _effectTimeLeft = _config.Duration;
                ApplyThirstAsync().Forget();
            }
            else
            {
                _effectTimeLeft = _config.Duration;
            }
            
            _itemStorage.RemoveItems(itemInfo, 1);
        }
        
        private async UniTask ApplyThirstAsync()
        {
            _model.ConsumptionMultiplyer *= _config.FoodConsumptionMultiplyer;
            
            while (_effectTimeLeft > 0 && _exhaustionController.IsEffectActive == false)
            {
                _effectTimeLeft -= Time.deltaTime;
                await UniTask.NextFrame();
            }

            _effectTimeLeft = 0;
            _model.ConsumptionMultiplyer /= _config.FoodConsumptionMultiplyer;
        }
    }
}