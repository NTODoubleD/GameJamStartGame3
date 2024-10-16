﻿using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game.Gameplay.Character;
using Game.Gameplay.Survival_Metrics.Configs;
using Game.Gameplay.SurvivalMechanics;
using Game.Gameplay.SurvivalMechanics.Frost;
using Game.Gameplay.SurvivalMeсhanics.Endurance;
using Game.Gameplay.SurvivalMeсhanics.Exhaustion;
using Game.Gameplay.SurvivalMeсhanics.Fatigue;
using Game.Gameplay.SurvivalMeсhanics.Frostbite;
using Game.Gameplay.SurvivalMeсhanics.Health;
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
            BindConfigs();

            Container.BindInterfacesAndSelfTo<PlayerMetricsModel>().AsSingle();
            Container.Bind<HungerModel>().AsSingle();
            
            Container.Bind<CharacterMovementSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<LowMetricEffectController>().AsSingle();
            
            BindControllers();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<FrostController>().AsSingle();
            Container.BindInterfacesAndSelfTo<FrostStarter>().AsSingle();
            Container.BindInterfacesAndSelfTo<FrostbiteController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HungerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ExhaustionController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnduranceConsumptionController>().AsSingle().NonLazy();
            Container.Bind<EnduranceRestoreController>().AsSingle().NonLazy();
            Container.Bind<HealthRestoreController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FatigueController>().AsSingle().NonLazy();
            Container.Bind<HeatController>().AsSingle().NonLazy();
            Container.Bind<FrostChangeObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EatingController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ThirstController>().AsSingle();
        }

        private void BindConfigs()
        {
            var resourceContainer = Container.Resolve<IResourcesContainer>();
            var configsResource = resourceContainer.GetResource<ConfigsResource>();
            
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
    }
}