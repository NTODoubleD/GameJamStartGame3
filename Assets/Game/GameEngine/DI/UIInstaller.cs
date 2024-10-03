using Game.UI;
using Zenject;

namespace Game.GameEngine.DI
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TooltipController>().AsSingle();
        }
    }
}