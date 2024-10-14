using System.Collections.Generic;
using Game.Gameplay.Items;
using Game.Gameplay.Survival_Meсhanics.Scripts.Common;

namespace Game.UI.Pages
{
    public class RadialMenuItemsUseObserver
    {
        private readonly List<IGameItemUseObserver> _gameItemUseObservers;

        public RadialMenuItemsUseObserver(List<IGameItemUseObserver> gameItemUseObservers)
        {
            _gameItemUseObservers = gameItemUseObservers;
        }
        
        public void OnItemUsed(GameItemInfo itemInfo)
        {
            foreach (var observer in _gameItemUseObservers)
                observer.OnItemUsed(itemInfo);
        }
    }
}