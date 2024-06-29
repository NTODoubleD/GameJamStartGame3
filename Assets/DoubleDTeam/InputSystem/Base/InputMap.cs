using System;
using UnityEngine;

namespace DoubleDTeam.InputSystem.Base
{
    public abstract class InputMap : MonoBehaviour
    {
        private bool _isActive = false;

        public bool IsActive => _isActive;

        public event Action<bool> ActiveStateChanged;

        public virtual void Initialize()
        {
        }

        protected virtual void Cancel()
        {
        }

        public virtual void Enable()
        {
            _isActive = true;
            ActiveStateChanged?.Invoke(_isActive);
        }

        public virtual void Disable()
        {
            _isActive = false;
            Cancel();
            ActiveStateChanged?.Invoke(_isActive);
        }

        private void Update()
        {
            if (_isActive)
                Tick();
        }

        protected virtual void Tick()
        {
        }
    }
}