using System.Collections.Generic;
using Game.Gameplay.Items;
using Game.Gameplay.Survival_Meсhanics.Scripts.Common;
using Game.Gameplay.SurvivalMechanics;
using Game.Infrastructure.Storage;

namespace Game.Gameplay.SurvivalMeсhanics.Hunger
{
    public class EatingController : IGameItemUseObserver
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
        

        public void OnItemUsed(GameItemInfo itemInfo)
        {
            if (_food.Contains(itemInfo) == false || _itemStorage.GetCount(itemInfo) == 0)
                return;

            float restoreValue = _eatingConfig.GetRestoreValue(itemInfo);
            _playerMetricsModel.Hunger += restoreValue;
            _itemStorage.RemoveItems(itemInfo, 1);
        }
    }
}