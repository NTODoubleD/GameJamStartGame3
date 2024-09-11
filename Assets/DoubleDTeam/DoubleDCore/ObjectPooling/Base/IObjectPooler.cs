using DoubleDCore.ObjectPooling.Data;

namespace DoubleDCore.ObjectPooling.Base
{
    public interface IObjectPooler
    {
        public bool Contains<T>() where T : PoolingObject;

        public void Register(PoolingObjectCreateInfo createInfo);

        public void Remove<T>() where T : PoolingObject;

        public T Get<T>() where T : PoolingObject;

        public void Return(PoolingObject poolingObject);

        public void Clear();
    }
}