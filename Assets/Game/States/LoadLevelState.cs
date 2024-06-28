using DoubleDTeam.StateMachine.Base;
using UnityEngine.SceneManagement;

namespace Game.States
{
    internal class LoadLevelState : IPayloadedState<int>
    {
        public void Enter(int payload)
        {
            SceneManager.LoadScene(payload);
        }

        public void Exit()
        {
        }
    }
}