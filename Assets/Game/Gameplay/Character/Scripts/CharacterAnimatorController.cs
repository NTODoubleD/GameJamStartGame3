using DoubleDCore.Attributes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CharacterAnimatorController : MonoBehaviour
{
    [SerializeField, ReadOnlyProperty] private Animator _animator;
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private float _animationSpeedMultiplier;

    [SerializeField] private GameObject _axe;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int PickingUp = Animator.StringToHash("PickingUp");
    private static readonly int Feed = Animator.StringToHash("Feed");
    private static readonly int Kill = Animator.StringToHash("Kill");
    private static readonly int Heal = Animator.StringToHash("Heal");
    private static readonly int Cut = Animator.StringToHash("Cut");

    private UnityAction _currentKillCallback;
    private UnityAction _currentHealCallback;
    private UnityAction _currentFeedCallback;
    private UnityAction _currentCutCallback;

    public event UnityAction StartedPickingUp;
    public event UnityAction PickedUp;

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

    public void AnimateFeeding(UnityAction endCallback = null)
    {
        _currentFeedCallback = endCallback;
        _animator.SetTrigger(Feed);
    }

    public void AnimateKilling(UnityAction endCallback = null)
    {
        _currentKillCallback = endCallback;
        _animator.SetTrigger(Kill);
    }

    public void AnimateHealing(UnityAction endCallback = null)
    {
        _currentHealCallback = endCallback;
        _animator.SetTrigger(Heal);
    }

    public void AnimateCutting(UnityAction endCallback = null)
    {
        _currentCutCallback = endCallback;
        _animator.SetTrigger(Cut);
    }

    #region ANIMATOR_EVENTS

    public void OnFootstep() =>
        SoundsManager.PlayFootstepSound(transform.position);

    public void OnStartedPickingUp() =>
        StartedPickingUp?.Invoke();

    public void OnPickedUp() =>
        PickedUp?.Invoke();

    public void OnFeeded()
    {
        _currentFeedCallback?.Invoke();
        _currentFeedCallback = null;
    }

    public void OnKilled()
    {
        _currentKillCallback?.Invoke();
        _currentKillCallback = null;
    }

    public void OnHealed()
    {
        _currentHealCallback?.Invoke();
        _currentHealCallback = null;
    }

    public void OnCutted()
    {
        _currentCutCallback?.Invoke();
        _currentCutCallback = null;
    }

    public void OnStartedInteraction() =>
        StartedInteraction?.Invoke();

    public void OnEndedInteraction() =>
        EndedInteraction?.Invoke();

    public void OnMeat1() =>
        SoundsManager.PlayMeat1(transform.position);

    public void OnMeat2() =>
        SoundsManager.PlayMeat2(transform.position);

    public void OnMeat3() =>
        SoundsManager.PlayMeat3(transform.position);

    public void OnGetMoch() =>
        SoundsManager.PlayMeat3(transform.position);

    public void OnKillOlen() =>
        SoundsManager.PlayKillOlen(transform.position);

    public void OnGetAxe() =>
        _axe.gameObject.SetActive(true);

    public void OnDissapearAxe() =>
        _axe.gameObject.SetActive(false);

    #endregion

    private void ChangeAnimationSpeed(float speed)
    {
        speed *= _animationSpeedMultiplier;
        _animator.SetFloat(Speed, speed);
    }
}