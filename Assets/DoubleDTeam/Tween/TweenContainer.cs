using System.Collections;
using System.Collections.Generic;
using DoubleDTeam.Containers.Base;
using UnityEngine;

namespace DoubleDTeam.Tween
{
    public class TweenContainer<T> : IModule where T : MonoBehaviour
    {
        private readonly Dictionary<T, Coroutine> _tweens = new();

        public void StartTween(T obj, IEnumerator enumerator)
        {
            KillTween(obj);

            var coroutine = obj.StartCoroutine(enumerator);

            _tweens.Add(obj, coroutine);
        }

        public void KillTween(T obj)
        {
            if (_tweens.ContainsKey(obj) == false)
                return;

            if (_tweens[obj] == null)
            {
                _tweens.Remove(obj);
                return;
            }

            obj.StopCoroutine(_tweens[obj]);
            _tweens.Remove(obj);
        }
    }
}