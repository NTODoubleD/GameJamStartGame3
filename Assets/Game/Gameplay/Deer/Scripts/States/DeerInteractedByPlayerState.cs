using DoubleDCore.Automat.Base;
using UnityEngine;

namespace Game.Gameplay.States
{
    public class DeerInteractedByPlayerState : IState
    {
        private readonly Transform _player;
        private readonly Transform _deer;

        public DeerInteractedByPlayerState(Transform player, Transform deer)
        {
            _player = player;
            _deer = deer;
        }
        
        public void Enter()
        {
            Vector3 playerXZ = _player.position;
            playerXZ.y = 0;
            
            Vector3 deerXZ = _deer.position;
            deerXZ.y = 0;
            
            Vector3 dir = playerXZ - deerXZ;
            _deer.forward = dir.normalized;
            _player.forward = -dir.normalized;
        }

        public void Exit()
        {
        }
    }
}