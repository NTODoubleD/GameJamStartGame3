using DoubleDTeam.StateMachine.Base;

namespace Game.Gameplay.States
{
    public class DeerDieState : IState
    {
        private readonly DeerAnimatorController _animatorController;
        private readonly DeerInfo _deerInfo;

        public DeerDieState(DeerAnimatorController animatorController, DeerInfo deerInfo) 
        {
            _animatorController = animatorController;
            _deerInfo = deerInfo;
        }

        public void Enter()
        {
            _animatorController.StartDead();
            _deerInfo.Status = DeerStatus.Dead;
        }

        public void Exit()
        {
            
        }
    }
}