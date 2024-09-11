using System;
using System.Collections.Generic;
using DoubleDCore.Automat.Base;

namespace DoubleDCore.Automat
{
    public class StateMachine : IFullStateMachine, IDisposable
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

        public TState GetState<TState>() where TState : class, IExitableState
        {
            var type = typeof(TState);

            if (_states.ContainsKey(type) == false)
                throw new Exception($"State {type.Name} does not exist");

            return _states[type] as TState;
        }

        public void Dispose()
        {
            _currentState?.Exit();
            _currentState = null;

            _states.Clear();
        }
    }
}