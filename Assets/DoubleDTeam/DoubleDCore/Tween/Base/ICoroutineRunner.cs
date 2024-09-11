using System.Collections;
using UnityEngine;

namespace DoubleDCore.Tween.Base
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator enumerator);
        public void StopCoroutine(IEnumerator enumerator);
        public void StopCoroutine(Coroutine enumerator);
    }
}