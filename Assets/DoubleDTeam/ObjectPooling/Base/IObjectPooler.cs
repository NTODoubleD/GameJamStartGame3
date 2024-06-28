using DoubleDTeam.Containers.Base;
using DoubleDTeam.ObjectPooling.Data;

namespace DoubleDTeam.ObjectPooling.Base
{
    public interface IObjectPooler : IModule
    {
        public bool Contains<T>() where T : PoolingObject;
        
        public void Register(PoolingObjectCreateInfo createInfo);

        public void Remove<T>() where T : PoolingObject;
        
        public T Get<T>() where T : PoolingObject;
        
        public void Return(PoolingObject poolingObject);

        public void Clear();
    }
}