using DoubleDTeam.Containers;
using DoubleDTeam.StateMachine;
using DoubleDTeam.StateMachine.Base;
using DoubleDTeam.UI.Base;
using Game.Gameplay.AI;
using Game.Gameplay.States;
using Game.UI.Pages;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Gameplay
{
    public class Deer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private DeerInteractive _deerInteractive;
        [SerializeField] private DeerAnimatorController _animatorController;

        public DeerInfo DeerInfo { get; private set; }
        public DeerInteractive DeerInteractive => _deerInteractive;

        private IUIManager _uiManager;

        private WalkablePlane _walkablePlane;
        private StateMachine _deerStateMachine;

        public event UnityAction<Deer> Died;

        private void Awake()
        {
            _uiManager = Services.ProjectContext.GetModule<IUIManager>();
            _walkablePlane = Services.SceneContext.GetModule<WalkablePlane>();

            _deerStateMachine = new StateMachine();

            _deerStateMachine.BindState(new DeerIdleState());
            _deerStateMachine.BindState(new DeerEatsState());
            _deerStateMachine.BindState(new DeerDieState(_animatorController, DeerInfo));
            _deerStateMachine.BindState(new DeerCutState(gameObject));
            _deerStateMachine.BindState(new DeerRandomWalkState(_navMeshAgent, _walkablePlane, this));
            _deerStateMachine.BindState(new DeerInteractedByPlayerState());
        }

        public void Initialize<TStartState>(DeerInfo deerInfo) where TStartState : class, IState
        {
            DeerInfo = deerInfo;
            _deerStateMachine.Enter<TStartState>();
        }

        private DeerInfoPageArgument GetDeerInfoPageArgument()
        {
            return new DeerInfoPageArgument()
            {
                Info = DeerInfo,
                OnClose = EnterWalkingState
            };
        }

        public void OpenInfoPage()
        {
            _uiManager.OpenPage<DeerInfoPage, DeerInfoPageArgument>(GetDeerInfoPageArgument());
        }

        #region STATE_METHODS

        public void EnterIdleState() =>
            _deerStateMachine.Enter<DeerIdleState>();

        public void EnterWalkingState() =>
            _deerStateMachine.Enter<DeerRandomWalkState>();

        public void Die()
        {
            _deerStateMachine.Enter<DeerDieState>();
            Died?.Invoke(this);
        }

        public void Cut()
        {
            _deerStateMachine.Enter<DeerCutState>();
        }    

        #endregion
    }
}