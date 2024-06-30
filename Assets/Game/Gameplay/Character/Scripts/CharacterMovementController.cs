using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using Game.InputMaps;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private CharacterAnimatorController _animatorController;

    private PlayerInputMap _inputMap;
    private Vector2 _inputDirection;
    private bool _canMove = true;

    private void Awake()
    {
        _inputMap = Services.ProjectContext.GetModule<InputController>().GetMap<PlayerInputMap>();
    }

    private void OnEnable()
    {
        _inputMap.Move.Performed += OnMove;
        _inputMap.Move.Canceled += OnMove;
        _animatorController.StartedInteraction += OnInteractionAnimationStarted;
        _animatorController.EndedInteraction += OnInteractionAnimationEnded;
    }

    private void OnDisable()
    {
        _inputMap.Move.Performed -= OnMove;
        _inputMap.Move.Canceled -= OnMove;
        _animatorController.StartedInteraction -= OnInteractionAnimationStarted;
        _animatorController.EndedInteraction -= OnInteractionAnimationEnded;
    }

    private void FixedUpdate()
    {
        if (_canMove)
            _mover.Move(_inputDirection);
    }

    private void OnMove(Vector2 inputDirection)
    {
        _inputDirection = inputDirection;
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