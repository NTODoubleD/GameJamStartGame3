using DoubleDCore.Automat;
using DoubleDCore.Automat.Base;
using DoubleDCore.Extensions;
using UnityEngine;

namespace Infrastructure
{
    public class GameStateMachine : StateMachineDecorator
    {
        public GameStateMachine(IFullStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter<TState>()
        {
            Debug.Log($"{typeof(TState).Name} enter".Color(Color.yellow));
            base.Enter<TState>();
        }

        public override void Enter<TState, TPayload>(TPayload payload)
        {
            Debug.Log($"{typeof(TState).Name} enter".Color(Color.yellow));
            base.Enter<TState, TPayload>(payload);
        }
    }
}