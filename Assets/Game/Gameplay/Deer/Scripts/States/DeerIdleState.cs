using System;
using System.Collections;
using DoubleDCore.Automat.Base;
using Game.Gameplay.Scripts.Configs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay.States
{
    public class DeerIdleState : IState
    {
        private readonly MonoBehaviour _coroutineRunner;
        private readonly RandomIdleConfig _config;

        public event Action Entered;
        public event Action Exited;
        public event Action Completed;

        public DeerIdleState(MonoBehaviour coroutineRunner, RandomIdleConfig config)
        {
            _coroutineRunner = coroutineRunner;
            _config = config;
        }
        
        public void Enter()
        {
            _coroutine = _coroutineRunner.StartCoroutine(StartRandomIdle());
            Entered?.Invoke();
        }

        public void Exit()
        {
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);
            
            Exited?.Invoke();
        }
        
        private Coroutine _coroutine;

        private IEnumerator StartRandomIdle()
        {
            yield return new WaitForSeconds(Random.Range(_config.MinSeconds, _config.MaxSeconds));
            Completed?.Invoke();
            Exit();
        }
    }
}