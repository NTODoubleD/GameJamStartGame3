using Game.UI;
using Game.UI.Pages;
using Zenject;

namespace Game.GameEngine.DI
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TooltipController>().AsSingle();
            Container.Bind<RadialMenuItemsUseObserver>().AsSingle();
            Container.Bind<RadialItemsMenuPageOpener>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AdditionalInfoOpener>().AsSingle().NonLazy();
        }
    }
}