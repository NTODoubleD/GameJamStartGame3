namespace Game.Gameplay.Buildings
{
    public interface IUpgradeConditionVisitor
    {
        void Visit(TownHallUpgradeCondition condition);
        void Visit(ResourcesUpgradeCondition condition);
    }
}