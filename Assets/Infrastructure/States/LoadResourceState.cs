using DoubleDCore.Automat.Base;
using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Zenject;

namespace Infrastructure.States
{
    public class LoadResourceState : IState
    {
        private readonly IResourcesContainer _container;
        private readonly IFullStateMachine _gameStateMachine;

        [Inject]
        public LoadResourceState(IResourcesContainer container, GameStateMachine gameStateMachine)
        {
            _container = container;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _container.AddResource(new ConfigsResource());

            _gameStateMachine.Enter<LoadSaveState>();
        }

        public void Exit()
        {
        }
    }
}