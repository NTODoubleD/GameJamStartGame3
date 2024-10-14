using System;
using DoubleDCore.Service;
using UnityEngine;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    public class HeatZone : MonoService
    {
        private bool _isEnabled;
        private bool _isEntered;

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }

            set
            {
                if (value && _isEntered)
                    Entered?.Invoke();

                _isEnabled = value;
            }
        }
        
        public event Action Entered;
        public event Action Exited;
        
        private void OnTriggerEnter(Collider other)
        {
            _isEntered = true;
            
            if (_isEnabled)
                Entered?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            _isEntered = false;
            
            if (_isEnabled)
                Exited?.Invoke();            
        }
    }
}