using UnityEngine.EventSystems;

namespace DoubleDCore.UI
{
    public class EventSystemProvider
    {
        public readonly EventSystem EventSystem;

        public EventSystemProvider(EventSystem eventSystem)
        {
            EventSystem = eventSystem;
        }
    }
}