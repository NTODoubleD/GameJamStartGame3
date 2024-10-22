using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game.Gameplay.CharacterCamera;
using Game.Gameplay.Crafting;
using Game.Gameplay.Configs;
using Game.Gameplay.Survival_Metrics.Configs;
using Game.Gameplay.SurvivalMechanics.Frost;
using Game.Gameplay.SurvivalMeсhanics.Endurance;
using Game.Gameplay.SurvivalMeсhanics.Exhaustion;
using Game.Gameplay.SurvivalMeсhanics.Fatigue;
using Game.Gameplay.SurvivalMeсhanics.Frostbite;
using Game.Gameplay.SurvivalMeсhanics.Hunger;
using Game.Gameplay.SurvivalMeсhanics.Thirst;
using Game.Notifications;
using Game.Tips.Configs;
using Game.UI.Pages;
using Zenject;

namespace Game.GameEngine.DI
{
    public class ConfigsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var resourceContainer = Container.Resolve<IResourcesContainer>();
            var configsResource = resourceContainer.GetResource<ConfigsResource>();
            
            BindCraftingConfigs(configsResource);
            BindSurvivalConfigs(configsResource);
            BindUIConfigs(configsResource);
            BindCommonGameplayConfigs(configsResource);
            BindDeerControlConfigs(configsResource);
        }

        private void BindCraftingConfigs(ConfigsResource configsResource)
        {
            CookingConfig cookingConfig = configsResource.GetConfig<CookingConfig>();
            Container.BindInstance(cookingConfig).AsSingle();
        }

        private void BindSurvivalConfigs(ConfigsResource configsResource)
        {
            PlayerMetricsConfig playerMetricsConfig = configsResource.GetConfig<PlayerMetricsConfig>();
            FrostConfig frostConfig = configsResource.GetConfig<FrostConfig>();
            FrostbiteConfig frostbiteConfig = configsResource.GetConfig<FrostbiteConfig>();
            HungerConfig hungerConfig = configsResource.GetConfig<HungerConfig>();
            ExhaustionConfig exhaustionConfig = configsResource.GetConfig<ExhaustionConfig>();
            EnduranceConfig enduranceConfig = configsResource.GetConfig<EnduranceConfig>();
            FatigueConfig fatigueConfig = configsResource.GetConfig<FatigueConfig>();
            RestConfig restConfig = configsResource.GetConfig<RestConfig>();
            HeatConfig heatConfig = configsResource.GetConfig<HeatConfig>();
            EatingConfig eatingConfig = configsResource.GetConfig<EatingConfig>();
            ThirstConfig thirstConfig = configsResource.GetConfig<ThirstConfig>();

            Container.BindInstance(playerMetricsConfig).AsSingle();
            Container.BindInstance(frostConfig).AsSingle();
            Container.BindInstance(frostbiteConfig).AsSingle();
            Container.BindInstance(hungerConfig).AsSingle();
            Container.BindInstance(exhaustionConfig).AsSingle();
            Container.BindInstance(enduranceConfig).AsSingle();
            Container.BindInstance(fatigueConfig).AsSingle();
            Container.BindInstance(restConfig).AsSingle();
            Container.BindInstance(heatConfig).AsSingle();
            Container.BindInstance(eatingConfig).AsSingle();
            Container.BindInstance(thirstConfig).AsSingle();
        }

        private void BindUIConfigs(ConfigsResource configsResource)
        {
            AdditionalInfoOpenConfig additionalInfoOpenConfig = configsResource.GetConfig<AdditionalInfoOpenConfig>();
            NotificationsConfig notificationsConfig = configsResource.GetConfig<NotificationsConfig>();

            Container.BindInstance(additionalInfoOpenConfig).AsSingle();
            Container.BindInstance(notificationsConfig).AsSingle();
        }

        private void BindCommonGameplayConfigs(ConfigsResource configsResource)
        {
            GameTrainingsConfig trainingsConfig = configsResource.GetConfig<GameTrainingsConfig>();
            CameraZoomConfig zoomConfig = configsResource.GetConfig<CameraZoomConfig>();
            
            Container.BindInstance(trainingsConfig).AsSingle();
            Container.BindInstance(zoomConfig).AsSingle();
        }

        private void BindDeerControlConfigs(ConfigsResource configsResource)
        {
            DeerAgeConfig deerAgeConfig = configsResource.GetConfig<DeerAgeConfig>();
            DeerHungerConfig deerHungerConfig = configsResource.GetConfig<DeerHungerConfig>();
            DeerIllnessesConfig deerIllnessesConfig = configsResource.GetConfig<DeerIllnessesConfig>();
            
            Container.BindInstance(deerAgeConfig).AsSingle();
            Container.BindInstance(deerHungerConfig).AsSingle();
            Container.BindInstance(deerIllnessesConfig).AsSingle();
        }
    }
}