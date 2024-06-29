using DoubleDTeam.Containers.Base;
using UnityEngine.EventSystems;

namespace DoubleDTeam.UI
{
    public class EventSystemProvider : IModule
    {
        public readonly EventSystem EventSystem;

        public EventSystemProvider(EventSystem eventSystem)
        {
            EventSystem = eventSystem;
        }
    }
}