using System;
using System.Collections.Generic;
using DoubleDCore.ObjectPooling.Base;

namespace DoubleDCore.ObjectPooling
{
    public class Pooler<TPoolingType> : IPooler<TPoolingType>
    {
        private readonly Queue<TPoolingType> _queue = new();

        public int Count()
        {
            return _queue.Count;
        }

        public void Push(TPoolingType obj)
        {
            _queue.Enqueue(obj);
        }

        public TPoolingType Get()
        {
            bool isSuccess = TryGet(out var result);

            if (isSuccess == false)
                throw new Exception("Pool is empty");

            return result;
        }

        public bool TryGet(out TPoolingType result)
        {
            result = default;

            if (Count() <= 0)
                return false;

            result = _queue.Dequeue();

            return true;
        }

        public void Return(TPoolingType obj)
        {
            Push(obj);
        }

        public void Clear()
        {
            _queue.Clear();
        }
    }
}