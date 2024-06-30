using DoubleDTeam.Attributes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CharacterAnimatorController : MonoBehaviour
{
    [SerializeField, ReadOnlyProperty] private Animator _animator;
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private float _animationSpeedMultiplier;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int PickingUp = Animator.StringToHash("PickingUp");
    private static readonly int Feed = Animator.StringToHash("Feed");
    private static readonly int Kill = Animator.StringToHash("Kill");
    private static readonly int Heal = Animator.StringToHash("Heal");

    public event UnityAction StartedPickingUp;
    public event UnityAction PickedUp;
    public event UnityAction Feeded;
    public event UnityAction Killed;
    public event UnityAction Healed;

    public event UnityAction StartedInteraction;
    public event UnityAction EndedInteraction;

    private SoundsManager SoundsManager => SoundsManager.Instance;
    
    private void OnValidate() => 
        _animator = GetComponent<Animator>();

    private void OnEnable()
    {
        _mover.SpeedChanged += ChangeAnimationSpeed;
        
        
    }

    private void OnDisable() => 
        _mover.SpeedChanged -= ChangeAnimationSpeed;

    public void AnimatePickingUp() =>
        _animator.SetTrigger(PickingUp);

    public void AnimateFeeding() =>
        _animator.SetTrigger(Feed);

    public void AnimateKilling() =>
        _animator.SetTrigger(Kill);

    public void AnimateHealing() =>
        _animator.SetTrigger(Heal);

    #region ANIMATOR_EVENTS

    public void OnFootstep() => 
        SoundsManager.PlayFootstepSound(transform.position);

    public void OnStartedPickingUp() =>
        StartedPickingUp?.Invoke();

    public void OnPickedUp() =>
        PickedUp?.Invoke();

    public void OnFeeded() =>
        Feeded?.Invoke();

    public void OnKilled() =>
        Killed?.Invoke();

    public void OnHealed() =>
        Healed?.Invoke();

    public void OnStartedInteraction() =>
        StartedInteraction?.Invoke();

    public void OnEndedInteraction() =>
        EndedInteraction?.Invoke();

    #endregion

    private void ChangeAnimationSpeed(float speed)
    {
        speed *= _animationSpeedMultiplier;
        _animator.SetFloat(Speed, speed);
    }

}
