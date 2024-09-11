using DoubleDCore.Service;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMover : MonoService
{
    [Header("References")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _orientation;

    [Header("Settings")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _minimalRotationDelta;
    [SerializeField] private bool _useGlobalForward;

    public event UnityAction<float> SpeedChanged;

    private void OnDisable()
    {
        SpeedChanged?.Invoke(0);
    }

    public void Move(Vector2 inputDirection)
    {
        Rotate(inputDirection);
        MoveRigidbody(inputDirection);
        ControlSpeed();

        SpeedChanged?.Invoke(_rigidbody.velocity.magnitude);
    }

    private void Rotate(Vector2 inputDir)
    {
        var rigidbodyPosition = _rigidbody.transform.position;
        var cameraPosition = _camera.position;
        var viewDir = rigidbodyPosition - new Vector3(cameraPosition.x, rigidbodyPosition.y, cameraPosition.z);

        _orientation.forward = _useGlobalForward ? Vector3.forward : viewDir;

        if (Mathf.Abs(inputDir.x) < _minimalRotationDelta)
            inputDir.x = 0;

        var direction = _orientation.forward * inputDir.y + _orientation.right * inputDir.x;

        if (direction != Vector3.zero)
            _rigidbody.transform.forward = Vector3.Slerp(_rigidbody.transform.forward, direction.normalized, Time.fixedDeltaTime * _rotationSpeed);
    }

    private void MoveRigidbody(Vector2 inputDir)
    {
        var moveDirection = _orientation.forward * inputDir.y + _orientation.right * inputDir.x;

        if (moveDirection != Vector3.zero)
            _rigidbody.AddForce(moveDirection.normalized * _movementSpeed * 10f, ForceMode.Force);
    }

    private void ControlSpeed()
    {
        var velocity = _rigidbody.velocity;
        var flatVel = new Vector3(velocity.x, 0, velocity.z);

        if (!(flatVel.magnitude > _movementSpeed))
            return;
        
        var limitedVel = flatVel.normalized * _movementSpeed;
        _rigidbody.velocity = limitedVel;
    }
}