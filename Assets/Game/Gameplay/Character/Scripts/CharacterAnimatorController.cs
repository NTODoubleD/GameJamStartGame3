using System;
using System.Collections;
using System.Collections.Generic;
using DoubleDTeam.Attributes;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimatorController : MonoBehaviour
{
    [SerializeField, ReadOnlyProperty] private Animator _animator;
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private float _animationSpeedMultiplier;
    
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void OnValidate()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _mover.SpeedChanged += ChangeAnimationSpeed;
    }

    private void OnDisable()
    {
        _mover.SpeedChanged -= ChangeAnimationSpeed;
    }

    private void ChangeAnimationSpeed(float speed)
    {
        speed *= _animationSpeedMultiplier;
        _animator.SetFloat(Speed, speed);
    }
}
