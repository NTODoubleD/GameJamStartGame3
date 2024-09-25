using System;
using DoubleDCore.Service;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Character
{
    public class CharacterActionsObserver : MonoService
    {
        private CharacterMovementController _movementController;

        private bool _isSprinting;

        public event Action StartSprinting;
        public event Action EndSprinting;
        
        [Inject]
        private void Init(CharacterMovementController movementController)
        {
            _movementController = movementController;
        }

        private void FixedUpdate()
        {
            bool isSprintingNow = IsSprintingAtTheMoment();

            if (_isSprinting == false && isSprintingNow)
                StartSprinting?.Invoke();
            else if (_isSprinting && isSprintingNow == false)
                EndSprinting?.Invoke();

            _isSprinting = isSprintingNow;
        }

        private bool IsSprintingAtTheMoment()
        {
            return _movementController.InputDirection != Vector2.zero 
                   && _movementController.CanMove &&
                   _movementController.IsSprint;
        }
    }
}