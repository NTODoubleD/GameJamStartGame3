using DoubleDTeam.StateMachine.Base;
using UnityEngine.SceneManagement;

namespace Game.States
{
    internal class LoadLevelState : IPayloadedState<string>
    {
        public void Enter(string payload)
        {
            SceneManager.LoadScene(payload);
        }

        public void Exit()
        {
        }
    }
}