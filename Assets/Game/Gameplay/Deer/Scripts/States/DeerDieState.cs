using DoubleDTeam.StateMachine.Base;
using UnityEngine.AI;

namespace Game.Gameplay.States
{
    public class DeerDieState : IState
    {
        private readonly DeerAnimatorController _animatorController;
        private readonly DeerInfo _deerInfo;
        private readonly NavMeshAgent _navMeshAgent;

        public DeerDieState(DeerAnimatorController animatorController, DeerInfo deerInfo, NavMeshAgent navMeshAgent)
        {
            _animatorController = animatorController;
            _deerInfo = deerInfo;
            _navMeshAgent = navMeshAgent;
        }

        public void Enter()
        {
            _animatorController.StartDead();
            _deerInfo.IsDead = true;
            _navMeshAgent.enabled = false;
        }

        public void Exit()
        {
        }
    }
}