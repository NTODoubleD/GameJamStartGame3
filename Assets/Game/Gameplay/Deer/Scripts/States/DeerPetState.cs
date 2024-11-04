using DoubleDCore.Automat.Base;
using UnityEngine;

namespace Game.Gameplay.States
{
    public class DeerPetState : IState
    {
        private readonly Transform _player;
        private readonly Transform _deer;

        public DeerPetState(Transform player, Transform deer)
        {
            _player = player;
            _deer = deer;
        }
        
        public void Exit()
        {
            
        }

        public void Enter()
        {
            var playerRight = _player.right;
            _deer.forward = -playerRight;

            var deerPos = _player.position + _player.forward * 0.85f + _player.right * 0.3f;
            _deer.position = deerPos;
        }
    }
}