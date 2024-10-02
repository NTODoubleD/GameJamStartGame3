using System;
using UnityEngine;

namespace Game.Gameplay.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class InteractionTrigger : MonoBehaviour
    {
        [SerializeField] private InteractiveObject _connectedObject;
        
        public InteractiveObject ConnectedObject => _connectedObject;

        public event Action<InteractionTrigger> Entered;
        public event Action<InteractionTrigger> Exited;
        public event Action<InteractionTrigger> Stayed;
        
        private void OnTriggerEnter(Collider other)
        {
            Entered?.Invoke(this);
        }

        private void OnTriggerStay(Collider other)
        {
            Stayed?.Invoke(this);
        }

        private void OnTriggerExit(Collider other)
        {
            Exited?.Invoke(this);
        }
    }
}