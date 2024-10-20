using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game.Gameplay.CharacterCamera;
using Game.Tips;
using Game.Tips.Configs;
using Zenject;
using Game.Gameplay.Survival_Meсhanics.Scripts.Common;
using Game.Notifications.Triggers;
using Game.Quests.Tasks;

namespace Game.GameEngine.DI
{
    public class CommonGameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            foreach (var task in FindObjectsOfType<ConsumeItemTask>())
                Container.Bind<IGameItemUseObserver>().To<ConsumeItemTask>().FromInstance(task).AsCached();

            BindConfigs();
            Container.BindInterfacesAndSelfTo<GameTrainingsStarter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraZoomController>().AsSingle().NonLazy();

            Container.Bind<DeerNotificationsController>().AsSingle().NonLazy();
        }
        
        private void BindConfigs()
        {
            var resourceContainer = Container.Resolve<IResourcesContainer>();
            var configsResource = resourceContainer.GetResource<ConfigsResource>();
            
            GameTrainingsConfig trainingsConfig = configsResource.GetConfig<GameTrainingsConfig>();
            CameraZoomConfig zoomConfig = configsResource.GetConfig<CameraZoomConfig>();
            
            Container.BindInstance(trainingsConfig).AsSingle();
            Container.BindInstance(zoomConfig).AsSingle();
        }
    }
}