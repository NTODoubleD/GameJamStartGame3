using UnityEngine;

namespace DoubleDTeam.ObjectPooling.Base
{
    public abstract class PoolingObject : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Spawn();
        public abstract void DeSpawn();
    }
}