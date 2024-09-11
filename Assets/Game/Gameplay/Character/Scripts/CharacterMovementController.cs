using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private CharacterAnimatorController _animatorController;

    private GameInput _inputController;

    private Vector2 _inputDirection;
    private bool _canMove = true;

    [Inject]
    private void Init(GameInput inputController)
    {
        _inputController = inputController;
    }

    private void OnEnable()
    {
        _inputController.Player.Move.performed += OnMove;
        _inputController.Player.Move.canceled += OnMove;
        _animatorController.StartedInteraction += OnInteractionAnimationStarted;
        _animatorController.EndedInteraction += OnInteractionAnimationEnded;
    }

    private void OnDisable()
    {
        _inputController.Player.Move.performed -= OnMove;
        _inputController.Player.Move.canceled -= OnMove;
        _animatorController.StartedInteraction -= OnInteractionAnimationStarted;
        _animatorController.EndedInteraction -= OnInteractionAnimationEnded;
    }

    private void FixedUpdate()
    {
        if (_canMove)
            _mover.Move(_inputDirection);
    }

    private void OnMove(InputAction.CallbackContext callbackContext)
    {
        _inputDirection = callbackContext.ReadValue<Vector2>();
    }

    private void OnInteractionAnimationStarted()
    {
        _canMove = false;
        _mover.Move(Vector2.zero);
    }

    private void OnInteractionAnimationEnded()
    {
        _mover.Move(Vector2.zero);
        _canMove = true;
    }
}