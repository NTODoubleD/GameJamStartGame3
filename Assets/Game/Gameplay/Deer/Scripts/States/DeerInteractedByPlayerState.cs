using DoubleDCore.Automat.Base;
using UnityEngine;

namespace Game.Gameplay.States
{
    public class DeerInteractedByPlayerState : IState
    {
        private readonly Transform _player;
        private readonly Transform _deer;
        private readonly DeerInfo _deerInfo;

        public DeerInteractedByPlayerState(Transform player, Transform deer, DeerInfo deerInfo)
        {
            _player = player;
            _deer = deer;
            _deerInfo = deerInfo;
        }
        
        public void Enter()
        {
            if (_deerInfo.IsDead)
                return;
            
            Vector3 playerXZ = _player.position;
            playerXZ.y = 0;
            
            Vector3 deerXZ = _deer.position;
            deerXZ.y = 0;
            
            Vector3 dir = playerXZ - deerXZ;
            _deer.forward = dir.normalized;
            // _player.forward = -dir.normalized;
        }

        public void Exit()
        {
        }
    }
}