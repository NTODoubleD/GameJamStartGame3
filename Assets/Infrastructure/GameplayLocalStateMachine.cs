using DoubleDCore.Automat;
using DoubleDCore.Automat.Base;

namespace Infrastructure
{
    public class GameplayLocalStateMachine : StateMachineDecorator
    {
        public GameplayLocalStateMachine(IFullStateMachine stateMachine) : base(stateMachine)
        {
        }
    }
}