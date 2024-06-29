using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using DoubleDTeam.StateMachine;
using DoubleDTeam.UI.Base;
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
            var uiManager = Services.ProjectContext.GetModule<IUIManager>();

            stateMachine.BindState(new BootstrapState(stateMachine));
            stateMachine.BindState(new MainGameState(inputController));
            stateMachine.BindState(new MainMenuState(inputController, uiManager));

            stateMachine.Enter<BootstrapState, int>(_nextSceneIndex);
        }
    }
}