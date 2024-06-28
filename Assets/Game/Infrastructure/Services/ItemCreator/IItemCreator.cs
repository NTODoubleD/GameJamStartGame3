using DoubleDTeam.Containers.Base;
using Game.Infrastructure.Items;

namespace Game.Infrastructure.ItemCreator
{
    public interface IItemCreator : IModule
    {
        public Item CreateItem(ItemInfo itemInfo);
        public Item CreateItem(string id);
        public void ReturnItem(Item item);
    }
}