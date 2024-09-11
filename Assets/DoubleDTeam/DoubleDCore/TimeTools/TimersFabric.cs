using Zenject;

namespace DoubleDCore.TimeTools
{
    public class TimersFabric : ITimersFactory
    {
        [Inject] private DiContainer _container;

        public Timer Create(TimeBindingType timeBinding)
        {
            var result = _container.Instantiate<Timer>();
            result.TimeBinding = timeBinding;

            return result;
        }
    }
}