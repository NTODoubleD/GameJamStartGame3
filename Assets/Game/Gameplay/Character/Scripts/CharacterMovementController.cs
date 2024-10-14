using DoubleDCore.Service;
using Game.Gameplay.Character;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CharacterMovementController : MonoService
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private CharacterAnimatorController _animatorController;
    
    private GameInput _inputController;
    private CharacterMovementSettings _movementSettings;
    
    public Vector2 InputDirection { get; private set; }
    public bool CanMove { get; private set; } = true;
    public bool IsSprint { get; private set; }

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
        IsSprint = true;

    private void OnSprintStop(InputAction.CallbackContext callbackContext) =>
        IsSprint = false;

    private void FixedUpdate()
    {
        if (CanMove)
            _mover.Move(InputDirection, _movementSettings.CanSprint && IsSprint);
    }

    private void OnMove(InputAction.CallbackContext callbackContext)
    {
        InputDirection = callbackContext.ReadValue<Vector2>();
    }

    private void OnInteractionAnimationStarted()
    {
        CanMove = false;
        _mover.Move(Vector2.zero, false);
    }

    private void OnInteractionAnimationEnded()
    {
        _mover.Move(Vector2.zero, false);
        CanMove = true;
    }
}