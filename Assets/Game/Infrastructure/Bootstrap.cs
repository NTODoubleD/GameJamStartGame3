using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
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
            var inputController = Services.ProjectContext.GetModule<InputController>();

            stateMachine.BindState(new BootstrapState(stateMachine));
            stateMachine.BindState(new LoadLevelState(stateMachine));
            stateMachine.BindState(new MainGameBootstrap(inputController));

            stateMachine.Enter<BootstrapState, int>(_nextSceneIndex);
        }
    }
}