namespace Game.Gameplay.Buildings
{
    public class ConditionResourcesSpender : IUpgradeConditionVisitor
    {
        void IUpgradeConditionVisitor.Visit(TownHallUpgradeCondition condition)
        {
            
        }

        void IUpgradeConditionVisitor.Visit(ResourcesUpgradeCondition condition)
        {
            //тратим ресурсы
        }
    }
}