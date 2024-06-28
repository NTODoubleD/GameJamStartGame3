using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;

    private void FixedUpdate()
    {
        Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _mover.Move(inputDirection);
    }
}