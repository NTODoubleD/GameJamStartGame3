using DoubleDCore.Automat;
using DoubleDCore.Automat.Base;
using DoubleDCore.Fabrics;
using DoubleDCore.Fabrics.Base;
using DoubleDCore.Finder;
using DoubleDCore.GameResources;
using DoubleDCore.GameResources.Base;
using DoubleDCore.Initialization;
using DoubleDCore.Initialization.Base;
using DoubleDCore.ObjectPooling;
using DoubleDCore.ObjectPooling.Base;
using DoubleDCore.PhysicsTools.Casting.Raycasting;
using DoubleDCore.QuestsSystem;
using DoubleDCore.QuestsSystem.Base;
using DoubleDCore.SaveSystem.Base;
using DoubleDCore.SaveSystem.Savers;
using DoubleDCore.TimeTools;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Base;
using DoubleDCore.TranslationTools.Extensions;
using DoubleDCore.Tween;
using DoubleDCore.Tween.Base;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private string _nextSceneIndex = "MainMenu";
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private CoroutineRunner _coroutineRunner;

        public override void InstallBindings()
        {
            RegisterServices();
        }

        public override async void Start()
        {
            base.Start();

            Initialize();
        }

        public void Initialize()
        {
            TranslationToolsExtensions.LanguageProvider = Container.Resolve<ILanguageProvider>();

            var stateMachine = InitializeStateMachine();

            stateMachine.Enter<BootstrapState>();
        }

        private IFullStateMachine InitializeStateMachine()
        {
            var gameStateMachine = Container.Resolve<GameStateMachine>();

            gameStateMachine.BindState(Container.Instantiate<BootstrapState>());
            gameStateMachine.BindState(Container.Instantiate<LoadResourceState>());
            gameStateMachine.BindState(Container.Instantiate<LoadSaveState>());
            gameStateMachine.BindState(Container.Instantiate<LoadSceneState>());
            gameStateMachine.BindState(Container.Instantiate<MainMenuState>());
            gameStateMachine.BindState(Container.Instantiate<LyricsState>());
            gameStateMachine.BindState(Container.Instantiate<GameplayState>());

            return gameStateMachine;
        }

        private void RegisterServices()
        {
            RegisterUtilities();
            RegisterFactories();
            SetSettings();

            Container.Bind<GameStateMachine>()
                .FromInstance(new GameStateMachine(new StateMachine())).AsSingle().NonLazy();
        }

        private void RegisterUtilities()
        {
            Container.Bind<BootstrapInfo>().FromInstance(new BootstrapInfo(_nextSceneIndex));

            Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();

            Container.Bind<IUIManager>().To<UIManager>().AsSingle();
            Container.Bind<ILanguageProvider>().To<MockLanguageProvider>().AsSingle();
            Container.Bind<IQuestController>().To<QuestController>().AsSingle();
            Container.Bind<IRayCaster>().To<RayCaster>().AsSingle();
            Container.Bind<IObjectPooler>().To<ObjectPooler>().AsSingle();
            Container.Bind<IResourcesContainer>().To<ResourcesContainer>().AsSingle();
            Container.Bind<IGameObjectFinder>().To<GameObjectFinder>().AsSingle();
            Container.Bind<ISaveController>().To<FileSaver>().AsSingle();

            Container.Bind<GameInput>().AsSingle();

            Container.Bind<IInitializeService>().To<InitializeService>().AsSingle();

            Container.Bind<EventSystemProvider>().FromInstance(new EventSystemProvider(_eventSystem)).AsSingle();
        }

        private void RegisterFactories()
        {
            Container.Bind<IPrefabFabric>().To<PrefabFabric>().AsSingle();
            Container.Bind<ITimersFactory>().To<TimersFabric>().AsSingle();
        }

        private void SetSettings()
        {
#if UNITY_EDITOR
            Application.targetFrameRate = 1000;
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.targetFrameRate = 60;
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.targetFrameRate = 60;
#endif
        }
    }
}