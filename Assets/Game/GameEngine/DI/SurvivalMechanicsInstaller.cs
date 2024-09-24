using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game.Gameplay.Character;
using Game.Gameplay.Survival_Metrics.Configs;
using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMechanics.Frost;
using Game.Gameplay.SurvivalMeсhanics.Frostbite;
using Zenject;

namespace Game.GameEngine.DI
{
    public class SurvivalMechanicsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var resourceContainer = Container.Resolve<IResourcesContainer>();
            var configsResource = resourceContainer.GetResource<ConfigsResource>();
            
            PlayerMetricsConfig playerMetricsConfig = configsResource.GetConfig<PlayerMetricsConfig>();
            FrostConfig frostConfig = configsResource.GetConfig<FrostConfig>();
            FrostbiteConfig frostbiteConfig = configsResource.GetConfig<FrostbiteConfig>();

            Container.BindInstance(playerMetricsConfig).AsSingle();
            Container.BindInstance(frostConfig).AsSingle();
            Container.BindInstance(frostbiteConfig).AsSingle();
            
            Container.Bind<PlayerMetricsModel>().AsSingle();
            Container.Bind<CharacterMovementSettings>().AsSingle();

            Container.Bind<FrostController>().AsSingle();
            Container.Bind<FrostbiteController>().AsSingle();
        }
    }
}