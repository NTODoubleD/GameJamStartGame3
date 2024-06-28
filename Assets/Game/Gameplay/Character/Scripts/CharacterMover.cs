using UnityEngine;
using UnityEngine.Events;

public class CharacterMover : MonoBehaviour
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
        Vector3 viewDir = _rigidbody.transform.position - new Vector3(_camera.transform.position.x, _rigidbody.transform.position.y, _camera.transform.position.z);

        if (_useGlobalForward == false)
            _orientation.forward = viewDir;
        else
            _orientation.forward = Vector3.forward;

        if (Mathf.Abs(inputDir.x) < _minimalRotationDelta)
            inputDir.x = 0;

        Vector3 direction = _orientation.forward * inputDir.y + _orientation.right * inputDir.x;

        if (direction != Vector3.zero)
            _rigidbody.transform.forward = Vector3.Slerp(_rigidbody.transform.forward, direction.normalized, Time.fixedDeltaTime * _rotationSpeed);
    }

    private void MoveRigidbody(Vector2 inputDir)
    {
        Vector3 moveDirection = _orientation.forward * inputDir.y + _orientation.right * inputDir.x;

        if (moveDirection != Vector3.zero)
        {
            _rigidbody.AddForce(moveDirection.normalized * _movementSpeed * 10, ForceMode.Force);
        }
    }

    private void ControlSpeed()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

        if (flatVel.magnitude > _movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _movementSpeed;
            _rigidbody.velocity = limitedVel;
        }
    }
}