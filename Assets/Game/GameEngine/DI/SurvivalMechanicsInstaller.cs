using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game.Gameplay.Survival_Metrics;
using Game.Gameplay.Survival_Metrics.Configs;
using Zenject;

namespace Game.GameEngine.DI
{
    public class SurvivalMechanicsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var resourceContainer = Container.Resolve<IResourcesContainer>();
            PlayerMetricsConfig playerMetricsConfig =
                resourceContainer.GetResource<ConfigsResource>().GetConfig<PlayerMetricsConfig>();

            Container.BindInstance(playerMetricsConfig).AsSingle();
            Container.Bind<PlayerMetricsModel>().AsSingle();
        }
    }
}