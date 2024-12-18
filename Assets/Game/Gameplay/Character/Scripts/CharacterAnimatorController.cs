using DoubleDCore.Attributes;
using DoubleDCore.Service;
using Game.Gameplay.Character;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CharacterAnimatorController : MonoService
{
    [SerializeField, ReadOnlyProperty] private Animator _animator;
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private CharacterAudio _characterAudio;
    [SerializeField] private float _animationSpeedMultiplier;

    [SerializeField] private GameObject _axe;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int PickingUp = Animator.StringToHash("PickingUp");
    private static readonly int Feed = Animator.StringToHash("Feed");
    private static readonly int Kill = Animator.StringToHash("Kill");
    private static readonly int Heal = Animator.StringToHash("Heal");
    private static readonly int Cut = Animator.StringToHash("Cut");
    private static readonly int Sit = Animator.StringToHash("Sit");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int BigPet = Animator.StringToHash("BigPet");
    private static readonly int MiniPet = Animator.StringToHash("MiniPet");

    private UnityAction _currentKillCallback;
    private UnityAction _currentHealCallback;
    private UnityAction _currentFeedCallback;
    private UnityAction _currentCutCallback;
    private UnityAction _currentMiniPetCallback;
    private UnityAction _currentBigPetCallback;

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

    public void AnimateMiniPetting(UnityAction endCallback = null)
    {
        _currentMiniPetCallback = endCallback;
        _animator.SetTrigger(MiniPet);
    }

    public void AnimateBigPetting(UnityAction endCallback = null)
    {
        _currentBigPetCallback = endCallback;
        _animator.SetTrigger(BigPet);
    }

    public void AnimateSitting()
    {
        _animator.SetTrigger(Sit);
        StartedInteraction?.Invoke();
    }

    public void AnimateStanding()
    {
        _animator.SetTrigger(Idle);
        EndedInteraction?.Invoke();
    }

    #region ANIMATOR_EVENTS

    public void OnFootstep() =>
        _characterAudio.PlayFootstepSound();

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

    public void OnMiniPettingEnded()
    {
        _currentMiniPetCallback?.Invoke();
        _currentMiniPetCallback = null;
    }

    public void OnBigPettingEnded()
    {
        _currentBigPetCallback?.Invoke();
        _currentBigPetCallback = null;
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