using System;
using DoubleDCore.Service;
using UnityEngine;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    public class HeatZone : MonoService
    {
        public event Action Entered;
        public event Action Exited; 
        
        private void OnTriggerEnter(Collider other)
        {
            Entered?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            Exited?.Invoke();            
        }
    }
}