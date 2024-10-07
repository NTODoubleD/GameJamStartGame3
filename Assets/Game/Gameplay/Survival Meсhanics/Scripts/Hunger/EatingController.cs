using System.Collections.Generic;
using Game.Gameplay.Items;
using Game.Gameplay.SurvivalMechanics;
using Game.Infrastructure.Storage;

namespace Game.Gameplay.SurvivalMeсhanics.Hunger
{
    public class EatingController
    {
        private readonly ItemStorage _itemStorage;
        private readonly EatingConfig _eatingConfig;
        private readonly PlayerMetricsModel _playerMetricsModel;

        private readonly HashSet<GameItemInfo> _food = new();

        public EatingController(ItemStorage itemStorage, EatingConfig eatingConfig, 
            PlayerMetricsModel playerMetricsModel)
        {
            _itemStorage = itemStorage;
            _eatingConfig = eatingConfig;
            _playerMetricsModel = playerMetricsModel;

            foreach (var foodItem in _eatingConfig.Food)
                _food.Add(foodItem);
        }
        
        public void TryEat(GameItemInfo food)
        {
            if (_food.Contains(food) == false || _itemStorage.GetCount(food) == 0)
                return;

            float restoreValue = _eatingConfig.GetRestoreValue(food);
            _playerMetricsModel.Hunger += restoreValue;
            _itemStorage.RemoveItems(food, 1);
        }
    }
}