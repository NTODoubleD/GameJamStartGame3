using DoubleDTeam.Containers.Base;

namespace DoubleDTeam.Containers
{
    public static class Services
    {
        public static readonly IServiceLocator ProjectContext = new ServiceLocator();
        public static readonly IServiceLocator SceneContext = new ServiceLocator();
    }
}