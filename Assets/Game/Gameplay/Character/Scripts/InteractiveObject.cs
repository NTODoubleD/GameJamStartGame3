using DoubleDTeam.Attributes;
using UnityEngine;

namespace Game.Gameplay.Character
{
    [RequireComponent(typeof(Collider))]
    public abstract class InteractiveObject : MonoBehaviour
    {
        [SerializeField, ReadOnlyProperty] private Collider _collider;

        private void OnValidate()
        {
            _collider = GetComponent<Collider>();
        }

        private void Awake()
        {
            _collider.isTrigger = true;
        }

        public abstract void Interact();
    }
}