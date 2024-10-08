using Game.Gameplay.Items;

namespace Game.Gameplay.Survival_Meсhanics.Scripts.Common
{
    public interface IGameItemUseObserver
    {
        void OnItemUsed(GameItemInfo itemInfo);
    }
}