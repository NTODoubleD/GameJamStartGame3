using Zenject;

namespace DoubleDCore.TimeTools
{
    public interface ITimersFactory : IFactory<TimeBindingType, Timer>
    {
    }
}