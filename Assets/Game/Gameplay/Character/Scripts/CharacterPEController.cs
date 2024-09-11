using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CharacterPEController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private CharacterMover _characterMover;
    
    private const int NormalEmissionROTSpeed = 20;
    private const int NormalEmissionRODSpeed = 10;
    private void OnValidate()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _characterMover = GetComponentInParent<CharacterMover>();
    }

    private void OnEnable()
    {
        _characterMover.SpeedChanged += SetSpeed;
    }

    private void OnDisable()
    {
        _characterMover.SpeedChanged -= SetSpeed;
    }

    public void SetSpeed(float speed)
    {
        var multiplier = speed < 1 ? 1 : speed;
    
        var emissionModule = _particleSystem.emission;

        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(NormalEmissionROTSpeed * multiplier);
        emissionModule.rateOverDistance = new ParticleSystem.MinMaxCurve(NormalEmissionRODSpeed * multiplier);
        
    }

}
