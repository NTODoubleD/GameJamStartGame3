using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game.Gameplay.Character;
using Game.Gameplay.Survival_Metrics.Configs;
using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMechanics.Frost;
using Game.Gameplay.SurvivalMeсhanics.Dehydration;
using Game.Gameplay.SurvivalMeсhanics.Exhaustion;
using Game.Gameplay.SurvivalMeсhanics.Frostbite;
using Game.Gameplay.SurvivalMeсhanics.Hunger;
using Game.Gameplay.SurvivalMeсhanics.PlayerMetrics;
using Game.Gameplay.SurvivalMeсhanics.Thirst;
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
            HungerConfig hungerConfig = configsResource.GetConfig<HungerConfig>();
            ExhaustionConfig exhaustionConfig = configsResource.GetConfig<ExhaustionConfig>();
            ThirstConfig thirstConfig = configsResource.GetConfig<ThirstConfig>();

            Container.BindInstance(playerMetricsConfig).AsSingle();
            Container.BindInstance(frostConfig).AsSingle();
            Container.BindInstance(frostbiteConfig).AsSingle();
            Container.BindInstance(hungerConfig).AsSingle();
            Container.BindInstance(exhaustionConfig).AsSingle();
            Container.BindInstance(thirstConfig).AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerMetricsModel>().AsSingle();
            Container.Bind<CharacterMovementSettings>().AsSingle();

            Container.BindInterfacesAndSelfTo<LowMetricEffectController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<FrostController>().AsSingle();
            Container.BindInterfacesAndSelfTo<FrostStarter>().AsSingle();
            Container.Bind<FrostbiteController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HungerController>().AsSingle().NonLazy();
            Container.Bind<ExhaustionController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ThirstController>().AsSingle().NonLazy();
            Container.Bind<DehydrationController>().AsSingle().NonLazy();
        }
    }
}