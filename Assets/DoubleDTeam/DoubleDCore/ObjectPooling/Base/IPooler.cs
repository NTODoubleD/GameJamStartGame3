namespace DoubleDCore.ObjectPooling.Base
{
    public interface IPooler<TPoolingType>
    {
        public int Count();

        public void Push(TPoolingType obj);

        public TPoolingType Get();

        public bool TryGet(out TPoolingType result);

        public void Return(TPoolingType obj);

        public void Clear();
    }
}