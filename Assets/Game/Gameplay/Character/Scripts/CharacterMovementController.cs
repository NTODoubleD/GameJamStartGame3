using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using Game.InputMaps;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;

    private PlayerInputMap _inputMap;

    private Vector2 _inputDirection;

    private void Awake()
    {
        _inputMap = Services.ProjectContext.GetModule<InputController>().GetMap<PlayerInputMap>();
    }

    private void OnEnable()
    {
        _inputMap.Move.Performed += OnMove;
        _inputMap.Move.Canceled += OnMove;
    }

    private void OnDisable()
    {
        _inputMap.Move.Performed -= OnMove;
        _inputMap.Move.Canceled -= OnMove;
    }

    private void OnMove(Vector2 inputDirection)
    {
        _inputDirection = inputDirection;
    }

    private void FixedUpdate()
    {
        _mover.Move(_inputDirection);
    }
}