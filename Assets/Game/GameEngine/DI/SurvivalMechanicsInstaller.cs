using Game.Gameplay.Character;
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
    }
}