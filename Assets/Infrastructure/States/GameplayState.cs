using DoubleDCore.Automat;
using DoubleDCore.Automat.Base;
using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game;
using Game.Infrastructure.Storage;
using Infrastructure.GameplayStates;
using Zenject;

namespace Infrastructure.States
{
    public class GameplayState : IState
    {
        private readonly DiContainer _container;
        private readonly IResourcesContainer _resourcesContainer;
        private readonly GameInput _gameInput;

        public GameplayState(DiContainer container, IResourcesContainer resourcesContainer, GameInput gameInput)
        {
            _container = container;
            _resourcesContainer = resourcesContainer;
            _gameInput = gameInput;
        }

        public void Enter()
        {
            var config = _resourcesContainer.GetResource<ConfigsResource>().GetConfig<GlobalConfig>();

            _container.Bind<ItemStorage>().FromInstance(new ItemStorage(config.TestInfo)).AsCached();

            var stateMachine = CreateLocalStateMachine();

            _container.Bind<GameplayLocalStateMachine>().FromInstance(stateMachine).AsCached();

            stateMachine.Enter<PlayerMovingState>();
        }

        public void Exit()
        {
            _container.Unbind<ItemStorage>();
            _container.Unbind<GameplayLocalStateMachine>();
        }

        private GameplayLocalStateMachine CreateLocalStateMachine()
        {
            var result = new GameplayLocalStateMachine(new StateMachine());

            result.BindState(new PlayerMovingState(_gameInput));
            result.BindState(new UIState(_gameInput));
            result.BindState(new MapState(_gameInput));

            return result;
        }
    }
}