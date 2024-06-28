using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;

namespace DoubleDTeam.StateMachine
{
    public class GameStateMachineInitializer : InitializeObject
    {
        public override void Initialize()
        {
            Services.ProjectContext.RegisterModule(new StateMachine());
        }

        public override void Deinitialize()
        {
            Services.ProjectContext.RemoveModule<StateMachine>();
        }
    }
}