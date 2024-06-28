using UnityEngine;

namespace DoubleDTeam.Containers.Base
{
    public abstract class InitializeObject : MonoBehaviour
    {
        public abstract void Initialize();

        public abstract void Deinitialize();
    }
}