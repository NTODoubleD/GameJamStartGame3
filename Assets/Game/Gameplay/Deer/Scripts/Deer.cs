using System;
using System.Collections.Generic;
using DoubleDCore.Automat;
using DoubleDCore.Automat.Base;
using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using DoubleDCore.UI.Base;
using Game.Gameplay.AI;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using Game.Gameplay.Scripts.Configs;
using Game.Gameplay.States;
using Game.UI.Pages;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Zenject;

namespace Game.Gameplay
{
    public class Deer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private DeerInteractive _deerInteractive;
        [SerializeField] private DeerAnimatorController _animatorController;
        [SerializeField] private DeerMeshing _deerMeshing;

        [Header("Movement Speed")] 
        [SerializeField] private float _normalSpeed;
        [SerializeField] private float _childSpeed;

        public DeerInfo DeerInfo { get; private set; }
        public DeerInteractive DeerInteractive => _deerInteractive;
        public DeerAnimatorController AnimatorController => _animatorController;
        public DeerMeshing DeerMeshing => _deerMeshing;

        private IUIManager _uiManager;

        private WalkablePlane _walkablePlane;
        private StateMachine _deerStateMachine;
        private DayCycleController _dayCycleController;
        private ConfigsResource _configsResource;
        private Transform _player;

        public event UnityAction<Deer> Died;
        public event UnityAction<Deer> Killed;
        public event UnityAction<Deer> Cutted; 
        public event UnityAction<Deer> Initialized;

        [Inject]
        private void Init(IUIManager uiManager, WalkablePlane walkablePlane, DayCycleController dayCycleController,
            IResourcesContainer resourcesContainer, CharacterMover player)
        {
            _uiManager = uiManager;
            _walkablePlane = walkablePlane;
            _dayCycleController = dayCycleController;
            _player = player.transform;

            _configsResource = resourcesContainer.GetResource<ConfigsResource>();
        }

        public void Initialize<TStartState>(DeerInfo deerInfo) where TStartState : class, IState
        {
            DeerInfo = deerInfo;
            _deerMeshing.ChangeMesh(deerInfo.Age, deerInfo.Gender);

            _navMeshAgent.speed = deerInfo.Age == DeerAge.Young ? _childSpeed : _normalSpeed;

            foreach (var ageDays in _ageTable.Keys)
            {
                if (_ageTable[ageDays] == deerInfo.Age)
                {
                    _age = ageDays;
                    break;
                }
            }

            deerInfo.AgeDays = _age;

            _deerStateMachine = new StateMachine();

            var randomWalkConfig = _configsResource.GetConfig<RandomWalkConfig>();
            var randomIdleConfig = _configsResource.GetConfig<RandomIdleConfig>();

            _deerStateMachine.BindState(new DeerIdleState(this, randomIdleConfig));
            _deerStateMachine.BindState(new DeerEatsState(_deerStateMachine));
            _deerStateMachine.BindState(new DeerDieState(_animatorController, DeerInfo, _navMeshAgent));
            _deerStateMachine.BindState(new DeerCutState(gameObject));
            _deerStateMachine.BindState(new DeerRandomWalkState(_navMeshAgent, _walkablePlane, this, randomWalkConfig));
            _deerStateMachine.BindState(new DeerInteractedByPlayerState(_player, transform, DeerInfo));

            _deerStateMachine.GetState<DeerIdleState>().Completed += OnIdleCompleted;
            _deerStateMachine.GetState<DeerRandomWalkState>().Completed += OnWalkCompleted;

            _deerStateMachine.Enter<TStartState>();

            Initialized?.Invoke(this);
        }

        private void OnDestroy()
        {
            _deerStateMachine.GetState<DeerIdleState>().Completed -= OnIdleCompleted;
            _deerStateMachine.GetState<DeerRandomWalkState>().Completed -= OnWalkCompleted;
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

        private readonly Dictionary<int, DeerAge> _ageTable = new()
        {
            { 2, DeerAge.Adult },
            { 5, DeerAge.Old },
            { 7, DeerAge.None }
        };

        public void AddAge()
        {
            if (DeerInfo.Status == DeerStatus.Killed)
                return;
            
            _age++;

            DeerInfo.AgeDays = _age;
            if (_ageTable.ContainsKey(_age) == false)
                return;

            if (_ageTable[_age] == DeerAge.None)
            {
                Die();
                return;
            }

            DeerInfo.Age = _ageTable[_age];
            _deerMeshing.ChangeMesh(DeerInfo.Age, DeerInfo.Gender);
            _navMeshAgent.speed = DeerInfo.Age == DeerAge.Young ? _childSpeed : _normalSpeed;
        }

        #region STATE_METHODS

        public void EnterIdleState()
        {
            if (DeerInfo.Status == DeerStatus.Killed)
                return;

            _deerStateMachine.Enter<DeerIdleState>();
        }

        public void EnterWalkingState()
        {
            if (DeerInfo.Status == DeerStatus.Killed)
                return;

            _deerStateMachine.Enter<DeerRandomWalkState>();
        }

        public void EnterUserInteractionState()
        {
            if (DeerInfo.Status == DeerStatus.Killed)
                return;

            _deerStateMachine.Enter<DeerInteractedByPlayerState>();
        }

        public void EnterEatState()
        {
            if (DeerInfo.Status == DeerStatus.Killed)
                return;

            _deerStateMachine.Enter<DeerEatsState>();
        }

        public void Die()
        {
            _deerStateMachine.Enter<DeerDieState>();

            DeerInfo.StatusBeforeDeath = DeerInfo.Status;
            DeerInfo.Status = DeerStatus.Killed;
            DeerInfo.DieDay = _dayCycleController.CurrentDay;

            Died?.Invoke(this);
        }

        public void Kill()
        {
            Killed?.Invoke(this);
            Die();
        }

        public void Cut()
        {
            Cutted?.Invoke(this);
            _deerStateMachine.Enter<DeerCutState>();
        }

        #endregion

        #region STATE_HANDLERS

        private void OnWalkCompleted() =>
            _deerStateMachine.Enter<DeerIdleState>();

        private void OnIdleCompleted() =>
            _deerStateMachine.Enter<DeerRandomWalkState>();

        #endregion
    }
}