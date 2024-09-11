using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoubleDCore.Tween
{
    public class TweenContainer<TBehaviour> where TBehaviour : MonoBehaviour
    {
        private readonly Dictionary<TBehaviour, Coroutine> _tweenList = new();

        public void StartTween(TBehaviour obj, IEnumerator enumerator)
        {
            KillTween(obj);

            if (obj == null || obj.enabled == false)
                return;

            var coroutine = obj.StartCoroutine(enumerator);

            _tweenList.Add(obj, coroutine);
        }

        public void KillTween(TBehaviour obj)
        {
            if (_tweenList.ContainsKey(obj) == false)
                return;

            if (_tweenList[obj] == null)
            {
                _tweenList.Remove(obj);
                return;
            }

            obj.StopCoroutine(_tweenList[obj]);
            _tweenList.Remove(obj);
        }
    }
}