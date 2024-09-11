using DoubleDCore.Automat.Base;
using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game;
using Game.Infrastructure.Storage;
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

            _container.Bind<ItemStorage>().FromInstance(new ItemStorage(config.TestInfo)).AsSingle();

            _gameInput.Player.Enable();
        }

        public void Exit()
        {
        }
    }
}