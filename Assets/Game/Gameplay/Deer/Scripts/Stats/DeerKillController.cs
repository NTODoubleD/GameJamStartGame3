using DoubleDCore.Service;
using UnityEngine;

namespace Game.Gameplay.Deers
{
    public class DeerKillController : MonoService
    {
        [SerializeField] private CharacterAnimatorController _characterAnimatorController;

        public bool CanKill(Deer deer)
        {
            return deer.DeerInfo.Age != DeerAge.Young && deer.DeerInfo.IsDead == false;
        }

        public void Kill(Deer deer)
        {
            if (CanKill(deer))
            {
                _characterAnimatorController.AnimateKilling(() => ApplyKill(deer));
            }
            else
            {
                Debug.LogError("CAN'T KILL THIS DEER");
            }
        }

        private void ApplyKill(Deer deer)
        {
            deer.Die();
        }
    }
}