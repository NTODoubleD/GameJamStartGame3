using DoubleDCore.Attributes;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class DeerAnimatorController : MonoBehaviour
{
    [SerializeField, ReadOnlyProperty] private Animator _animator;

    [SerializeField] private NavMeshAgent _agent;

    private static readonly int Dead = Animator.StringToHash("Death");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Eat = Animator.StringToHash("Eat");
    private SoundsManager SoundsManager => SoundsManager.Instance;

    private void OnValidate()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetSpeed(_agent.velocity.magnitude / _agent.speed);
    }

    public void StartDead()
    {
        _animator.SetTrigger(Dead);
    }

    public void StartEat()
    {
        _animator.SetTrigger(Eat);
    }

    public void SetSpeed(float value)
    {
        _animator.SetFloat(Speed, value);
    }

    public void OnEat()
    {
        SoundsManager.PlayDeerEat(transform.position);
    }
}