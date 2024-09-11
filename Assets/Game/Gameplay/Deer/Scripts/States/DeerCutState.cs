using DoubleDCore.Automat.Base;
using UnityEngine;

namespace Game.Gameplay.States
{
    public class DeerCutState : IState
    {
        private readonly GameObject _deerObject;

        public DeerCutState(GameObject deerObject)
        {
            _deerObject = deerObject;
        }

        public void Enter()
        {
            GameObject.Destroy(_deerObject);
        }

        public void Exit()
        {
        }
    }
}