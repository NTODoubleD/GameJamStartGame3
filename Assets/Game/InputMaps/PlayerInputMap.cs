using DoubleDTeam.InputSystem.Base;
using UnityEngine;

namespace Game.InputMaps
{
    public class PlayerInputMap : InputMap
    {
        public readonly InputCharacter Interact = new();

        public readonly PayloadedInputCharacter<Vector2> Move = new();

        public readonly InputCharacter LeftClick = new();
        public readonly PayloadedInputCharacter<Vector2> MousePosition = new();

        protected override void Tick()
        {
            MoveHandler();

            MouseHandler();

            InteractHandler();
        }

        protected override void Cancel()
        {
            Interact.CallCancel();
            Move.CallCancel(Vector2.zero);
            LeftClick.CallCancel();
            MousePosition.CallCancel(Vector2.zero);
        }

        private void InteractHandler()
        {
            if (Input.GetKeyDown(KeyCode.E))
                Interact.CallStart();

            if (Input.GetKey(KeyCode.E))
                Interact.CallPerform();

            if (Input.GetKeyUp(KeyCode.E))
                Interact.CallCancel();
        }

        private void MoveHandler()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            var direction = new Vector2(horizontal, vertical);
            direction.Normalize();

            Move.CallPerform(direction);
        }

        private void MouseHandler()
        {
            if (Input.GetMouseButtonDown(0))
                LeftClick.CallStart();

            if (Input.GetMouseButton(0))
                LeftClick.CallPerform();

            if (Input.GetMouseButtonUp(0))
                LeftClick.CallCancel();

            MousePosition.CallPerform(Input.mousePosition);
        }
    }
}