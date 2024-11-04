using Game.Gameplay.Deers;
using Zenject;

namespace Game.GameEngine.DI
{
    public class DeerControlInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<DeerAgeController>().AsSingle().NonLazy();
            Container.Bind<DeerBornController>().AsSingle().NonLazy();
            Container.Bind<DeerHungerController>().AsSingle().NonLazy();
            Container.Bind<DeerIllnessesController>().AsSingle().NonLazy();
            
            Container.Bind<DeerCutController>().AsSingle();
            Container.Bind<DeerPetController>().AsSingle();
        }
    }
}