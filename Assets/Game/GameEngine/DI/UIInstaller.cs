using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game.Notifications;
using Game.UI;
using Game.UI.Pages;
using Zenject;

namespace Game.GameEngine.DI
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var resourceContainer = Container.Resolve<IResourcesContainer>();
            var configsResource = resourceContainer.GetResource<ConfigsResource>();
            
            AdditionalInfoOpenConfig additionalInfoOpenConfig = configsResource.GetConfig<AdditionalInfoOpenConfig>();
            NotificationsConfig notificationsConfig = configsResource.GetConfig<NotificationsConfig>();

            Container.BindInstance(additionalInfoOpenConfig).AsSingle();
            Container.BindInstance(notificationsConfig).AsSingle();
            
            Container.Bind<TooltipController>().AsSingle();
            Container.Bind<RadialMenuItemsUseObserver>().AsSingle();
            Container.Bind<RadialItemsMenuPageOpener>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AdditionalInfoOpener>().AsSingle().NonLazy();
        }
    }
}