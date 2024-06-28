using DoubleDTeam.Containers.Base;

namespace DoubleDTeam.Containers.Initializers
{
    public class DontDestroyInitializer : InitializeObject
    {
        public override void Initialize()
        {
            DontDestroyOnLoad(gameObject);
        }

        public override void Deinitialize()
        {
        }
    }
}