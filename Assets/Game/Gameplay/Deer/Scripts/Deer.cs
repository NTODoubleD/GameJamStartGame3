using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.StateMachine;
using DoubleDTeam.StateMachine.Base;
using DoubleDTeam.UI.Base;
using Game.Gameplay.AI;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
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
        [SerializeField] private DeerMeshing _deerMeshing;

        public DeerInfo DeerInfo { get; private set; }
        public DeerInteractive DeerInteractive => _deerInteractive;
        public DeerAnimatorController AnimatorController => _animatorController;

        private IUIManager _uiManager;

        private WalkablePlane _walkablePlane;
        private StateMachine _deerStateMachine;
        private DayCycleController _dayCycleController;

        public event UnityAction<Deer> Died;
        public event UnityAction<Deer> Initialized;

        private void Awake()
        {
            _uiManager = Services.ProjectContext.GetModule<IUIManager>();
            _walkablePlane = Services.SceneContext.GetModule<WalkablePlane>();
            _dayCycleController = Services.SceneContext.GetModule<DayCycleController>();
        }

        public void Initialize<TStartState>(DeerInfo deerInfo) where TStartState : class, IState
        {
            DeerInfo = deerInfo;
            _deerMeshing.ChangeMesh(deerInfo.Age, deerInfo.Gender);

            if (deerInfo.Age == DeerAge.Adult)
                _age = 2;

            _deerStateMachine = new StateMachine();

            _deerStateMachine.BindState(new DeerIdleState());
            _deerStateMachine.BindState(new DeerEatsState());
            _deerStateMachine.BindState(new DeerDieState(_animatorController, DeerInfo, _navMeshAgent));
            _deerStateMachine.BindState(new DeerCutState(gameObject));
            _deerStateMachine.BindState(new DeerRandomWalkState(_navMeshAgent, _walkablePlane, this));
            _deerStateMachine.BindState(new DeerInteractedByPlayerState());

            _deerStateMachine.Enter<TStartState>();

            Initialized?.Invoke(this);
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

        private void OnEnable()
        {
            _dayCycleController.DayStarted += AddAge;
        }

        private void OnDisable()
        {
            _dayCycleController.DayStarted -= AddAge;
        }

        private int _age = 0;

        private Dictionary<int, DeerAge> _ageTable = new()
        {
            { 2, DeerAge.Adult },
            { 5, DeerAge.Old },
            { 7, DeerAge.None }
        };

        public void AddAge()
        {
            _age++;

            if (_ageTable.ContainsKey(_age) == false)
                return;

            if (_ageTable[_age] == DeerAge.None)
            {
                Die();
                return;
            }

            DeerInfo.Age = _ageTable[_age];
            _deerMeshing.ChangeMesh(DeerInfo.Age, DeerInfo.Gender);
        }

        #region STATE_METHODS

        public void EnterIdleState() =>
            _deerStateMachine.Enter<DeerIdleState>();

        public void EnterWalkingState() =>
            _deerStateMachine.Enter<DeerRandomWalkState>();

        public void Die()
        {
            _deerStateMachine.Enter<DeerDieState>();

            DeerInfo.Status = DeerStatus.Killed;

            Died?.Invoke(this);
        }

        public void Cut()
        {
            _deerStateMachine.Enter<DeerCutState>();
        }

        #endregion
    }
}