using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using DoubleDTeam.PhysicsTools.CollisionImpacts;
using Game.InputMaps;
using UnityEngine;

namespace Game.Gameplay.Character
{
    public class CharacterInteractor : TriggerListener<InteractiveObject>
    {
        private InteractiveObject _current;

        private PlayerInputMap _input;

        private void Awake()
        {
            _input = Services.ProjectContext.GetModule<InputController>().GetMap<PlayerInputMap>();
        }

        private void OnEnable()
        {
            _input.Interact.Started += OnInputInteract;
        }

        private void OnDisable()
        {
            _input.Interact.Started -= OnInputInteract;
        }

        private void OnInputInteract()
        {
            if (_current != null)
                _current.Interact();
        }

        protected override bool IsTarget(Collider col, out InteractiveObject target)
        {
            return col.TryGetComponent(out target);
        }

        protected override void OnTriggerStart(InteractiveObject target)
        {
            _current = target;
        }

        protected override void OnTriggerEnd(InteractiveObject target)
        {
            _current = null;
        }
    }
}