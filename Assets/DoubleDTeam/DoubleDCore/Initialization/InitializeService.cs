using DoubleDCore.Finder;
using DoubleDCore.Initialization.Base;
using Zenject;

namespace DoubleDCore.Initialization
{
    public class InitializeService : IInitializeService
    {
        private readonly IGameObjectFinder _finder;

        [Inject]
        public InitializeService(IGameObjectFinder finder)
        {
            _finder = finder;
        }

        public void InitializeAll<TType>() where TType : IInitializing
        {
            var initializingObjects = _finder.Find<TType>();

            foreach (var initializing in initializingObjects)
                initializing.Initialize();
        }

        public void Initialize(IInitializing initializingObject)
        {
            initializingObject.Initialize();
        }
    }
}