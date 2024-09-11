using Game.Infrastructure.Storage;
using Zenject;

namespace Game.Gameplay.Buildings
{
    public class ConditionResourcesSpender : IUpgradeConditionVisitor
    {
        private ItemStorage _itemStorage;

        [Inject]
        private void Init(ItemStorage itemStorage)
        {
            _itemStorage = itemStorage;
        }

        void IUpgradeConditionVisitor.Visit(TownHallUpgradeCondition condition)
        {
        }

        void IUpgradeConditionVisitor.Visit(ResourcesUpgradeCondition condition)
        {
            foreach (var keyPair in condition.NeccessaryItems)
                _itemStorage.RemoveItems(keyPair.Key, keyPair.Value);
        }
    }
}