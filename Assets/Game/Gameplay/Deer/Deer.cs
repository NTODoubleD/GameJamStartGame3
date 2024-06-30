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

            _deerStateMachine.BindState(new DeerEatsState());
            _deerStateMachine.BindState(new DeerrRandomWalkState(_navMeshAgent, _walkablePlane, this));
            _deerStateMachine.BindState(new DeerInteractedByPlayerState());
        }

        private void Start()
        {
            _deerStateMachine.Enter<DeerrRandomWalkState>();
        }

        private DeerInfo GetDeerInfo()
        {
            return new DeerInfo()
            {
                Name = "Max",
                Age = 2,
                HungerDegree = 0.5f,
                Gender = GenderType.Male,
                Status = DeerStatus.Standard,
                WorldPosition = transform.position
            };
        }

        public void OpenInfoPage()
        {
            _uiManager.OpenPage<DeerInfoPage, DeerInfo>(DeerInfo);
        }
    }
}