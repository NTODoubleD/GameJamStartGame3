using Game.Gameplay.Character;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private CharacterAnimatorController _animatorController;
    
    private GameInput _inputController;
    private CharacterMovementSettings _movementSettings;

    private Vector2 _inputDirection;
    private bool _canMove = true;
    private bool _isSprint;

    [Inject]
    private void Init(GameInput inputController, CharacterMovementSettings movementSettings)
    {
        _inputController = inputController;
        _movementSettings = movementSettings;
    }

    private void OnEnable()
    {
        _inputController.Player.Move.performed += OnMove;
        _inputController.Player.Move.canceled += OnMove;

        _inputController.Player.Sprint.started += OnSprintStart;
        _inputController.Player.Sprint.canceled += OnSprintStop;

        _animatorController.StartedInteraction += OnInteractionAnimationStarted;
        _animatorController.EndedInteraction += OnInteractionAnimationEnded;
    }

    private void OnDisable()
    {
        _inputController.Player.Move.performed -= OnMove;
        _inputController.Player.Move.canceled -= OnMove;

        _inputController.Player.Sprint.started -= OnSprintStart;
        _inputController.Player.Sprint.canceled -= OnSprintStop;

        _animatorController.StartedInteraction -= OnInteractionAnimationStarted;
        _animatorController.EndedInteraction -= OnInteractionAnimationEnded;
    }

    private void OnSprintStart(InputAction.CallbackContext callbackContext) =>
        _isSprint = true;

    private void OnSprintStop(InputAction.CallbackContext callbackContext) =>
        _isSprint = false;

    private void FixedUpdate()
    {
        if (_canMove)
            _mover.Move(_inputDirection, _movementSettings.CanSprint && _isSprint);
    }

    private void OnMove(InputAction.CallbackContext callbackContext)
    {
        _inputDirection = callbackContext.ReadValue<Vector2>();
    }

    private void OnInteractionAnimationStarted()
    {
        _canMove = false;
        _mover.Move(Vector2.zero, false);
    }

    private void OnInteractionAnimationEnded()
    {
        _mover.Move(Vector2.zero, false);
        _canMove = true;
    }
}