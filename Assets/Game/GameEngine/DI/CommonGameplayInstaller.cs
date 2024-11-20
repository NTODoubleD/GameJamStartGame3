using Game.Feedbacks;
using Game.Gameplay.CharacterCamera;
using Game.Tips;
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

            Container.BindInterfacesAndSelfTo<GameTrainingsStarter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraZoomController>().AsSingle().NonLazy();

            Container.Bind<DeerNotificationsController>().AsSingle().NonLazy();
            Container.Bind<StormFeedback>().FromComponentInHierarchy().AsSingle();
        }
    }
}