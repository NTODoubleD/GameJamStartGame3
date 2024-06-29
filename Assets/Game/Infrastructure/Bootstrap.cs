using DoubleDTeam.Containers;
using DoubleDTeam.StateMachine;
using Game.States;
using UnityEngine;

namespace Game.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private int _nextSceneIndex = 1;

        private void Start()
        {
            var stateMachine = Services.ProjectContext.GetModule<StateMachine>();

            stateMachine.BindState(new BootstrapState(stateMachine));
            stateMachine.BindState(new LoadLevelState());

            stateMachine.Enter<BootstrapState, int>(_nextSceneIndex);
        }
    }
}