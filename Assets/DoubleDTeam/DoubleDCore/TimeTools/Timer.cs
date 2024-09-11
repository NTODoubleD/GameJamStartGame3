using System;
using System.Collections;
using DoubleDCore.Tween.Base;
using UnityEngine;
using Zenject;

namespace DoubleDCore.TimeTools
{
    public class Timer
    {
        public float RemainingTime { get; private set; }

        private bool _isWorked;

        public bool IsWorked => _isWorked;

        public event Action<float> Started;
        public event Action<float> Stopped;
        public event Action Performed;

        private ICoroutineRunner _coroutineRunner;

        public TimeBindingType TimeBinding { get; set; }

        [Inject]
        private void Init(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Start(float time, Action onEnd = null)
        {
            if (_coroutine != null)
                Stop();

            _coroutine = _coroutineRunner.StartCoroutine(StartTimer(time, onEnd));
            _isWorked = true;

            Started?.Invoke(time);
        }

        public void Stop()
        {
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);

            _isWorked = false;
            _coroutine = null;

            Stopped?.Invoke(RemainingTime);
        }

        private Coroutine _coroutine;

        private IEnumerator StartTimer(float time, Action onEnd)
        {
            RemainingTime = time;

            while (RemainingTime >= 0)
            {
                yield return null;

                float pastTime = TimeBinding switch
                {
                    TimeBindingType.RealTime => Time.unscaledDeltaTime,
                    TimeBindingType.ScaledTime => Time.deltaTime,
                    TimeBindingType.FixedTime => Time.fixedDeltaTime,
                    _ => throw new ArgumentOutOfRangeException($"{TimeBinding} is not supported")
                };

                RemainingTime -= pastTime;

                if (RemainingTime > 0)
                    continue;

                RemainingTime = 0;

                Stop();

                onEnd?.Invoke();

                Performed?.Invoke();

                yield break;
            }
        }
    }
}