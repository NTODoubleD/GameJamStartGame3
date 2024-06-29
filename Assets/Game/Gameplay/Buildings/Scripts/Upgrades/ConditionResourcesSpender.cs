using DoubleDTeam.Containers;
using Game.Infrastructure.Storage;

namespace Game.Gameplay.Buildings
{
    public class ConditionResourcesSpender : IUpgradeConditionVisitor
    {
        void IUpgradeConditionVisitor.Visit(TownHallUpgradeCondition condition)
        {
            
        }

        void IUpgradeConditionVisitor.Visit(ResourcesUpgradeCondition condition)
        {
            var storage = Services.ProjectContext.GetModule<ItemStorage>();

            foreach (var keyPair in condition.NeccessaryItems)
                storage.RemoveItems(keyPair.Key, keyPair.Value);
        }
    }
}