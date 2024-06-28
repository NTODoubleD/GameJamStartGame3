using System;
using System.Collections.Generic;
using DoubleDTeam.StateMachine.Base;

namespace DoubleDTeam.StateMachine
{
    public class GameStateMachine
    {
        private readonly IStateFactory _stateFactory;
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(IStateFactory stateFactory) =>
            _stateFactory = stateFactory;

        public void CreateStates()
        {
            Dictionary<Type, IExitableState> states = _stateFactory.CreateStates();
            _states = states;
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = LoadState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = LoadState<TState>();
            state.Enter(payload);
        }

        private TState LoadState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}