using DoubleDTeam.Containers;
using DoubleDTeam.StateMachine;
using DoubleDTeam.UI.Base;
using Game.Gameplay.AI;
using Game.Gameplay.States;
using Game.UI.Pages;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay
{
    public class Deer : MonoBehaviour
    {
        [SerializeField] private WalkablePlane _walkablePlane;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public DeerInfo DeerInfo => GetDeerInfo();
        private IUIManager _uiManager;

        private StateMachine _deerStateMachine;

        private void Awake()
        {
            _uiManager = Services.ProjectContext.GetModule<IUIManager>();

            _deerStateMachine = new StateMachine();

            _deerStateMachine.BindState(new DeerIdleState());
            _deerStateMachine.BindState(new DeerEatsState());
            _deerStateMachine.BindState(new DeerRandomWalkState(_navMeshAgent, _walkablePlane, this));
            _deerStateMachine.BindState(new DeerInteractedByPlayerState());
        }

        private void Start()
        {
            _deerStateMachine.Enter<DeerRandomWalkState>();
        }

        private DeerInfo GetDeerInfo()
        {
            return new DeerInfo()
            {
                Name = "Max",
                Age = DeerAge.Adult,
                HungerDegree = 0.5f,
                Gender = GenderType.Male,
                Status = DeerStatus.Standard,
                WorldPosition = transform.position,
                OnEnd = EnterWalkingState
            };
        }

        public void OpenInfoPage()
        {
            _uiManager.OpenPage<DeerInfoPage, DeerInfo>(DeerInfo);
        }

        #region STATE_METHODS

        public void EnterIdleState() =>
            _deerStateMachine.Enter<DeerIdleState>();

        public void EnterWalkingState() =>
            _deerStateMachine.Enter<DeerRandomWalkState>();

        #endregion
    }
}