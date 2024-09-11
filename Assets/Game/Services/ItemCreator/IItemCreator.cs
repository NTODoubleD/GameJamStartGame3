using Game.Infrastructure.Items;

namespace Game.ItemCreator
{
    public interface IItemCreator
    {
        public Item CreateItem(ItemInfo itemInfo);
        public Item CreateItem(string id);
        public void ReturnItem(Item item);
    }
}