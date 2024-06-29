using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DoubleDTeam.UI
{
    public class EventSystemProviderInitializer : InitializeObject
    {
        [SerializeField] private EventSystem _eventSystem;

        public override void Initialize()
        {
            Services.ProjectContext.RegisterModule(new EventSystemProvider(_eventSystem));
        }

        public override void Deinitialize()
        {
            Services.ProjectContext.RemoveModule<EventSystemProvider>();
        }
    }
}