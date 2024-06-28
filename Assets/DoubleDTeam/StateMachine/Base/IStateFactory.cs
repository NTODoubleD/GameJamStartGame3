using System;
using System.Collections.Generic;

namespace DoubleDTeam.StateMachine.Base
{
    public interface IStateFactory
    {
        Dictionary<Type, IExitableState> CreateStates();
    }
}