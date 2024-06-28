using System;
using System.Collections.Generic;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.StateMachine.Base;

namespace DoubleDTeam.StateMachine
{
    public class StateMachine : IModule
    {
        private readonly Dictionary<Type, IExitableState> _states = new();

        private IExitableState _currentState;
        public IExitableState CurrentState => _currentState;

        public void BindState(IExitableState state)
        {
            _states.Add(state.GetType(), state);
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