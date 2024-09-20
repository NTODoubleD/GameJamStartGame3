using System;
using System.Collections;
using DoubleDCore.Automat.Base;
using Game.Gameplay.AI;
using Game.Gameplay.Scripts.Configs;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Game.Gameplay.States
{
    public class DeerRandomWalkState : IState
    {
        private readonly WalkablePlane _walkablePlane;
        private readonly NavMeshAgent _agent;
        private readonly MonoBehaviour _coroutineRunner;
        private readonly RandomWalkConfig _config;

        public event Action Entered;
        public event Action Exited;
        public event Action Completed;

        public DeerRandomWalkState(NavMeshAgent agent, WalkablePlane walkablePlane, 
            MonoBehaviour coroutineRunner, RandomWalkConfig config)
        {
            _agent = agent;
            _walkablePlane = walkablePlane;
            _coroutineRunner = coroutineRunner;
            _config = config;
        }

        public void Enter()
        {
            _agent.isStopped = false;
            _coroutine = _coroutineRunner.StartCoroutine(StartRandomWalk());
            Entered?.Invoke();
        }

        public void Exit()
        {
            _agent.isStopped = true;
            
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);
            
            Exited?.Invoke();
        }

        private Coroutine _coroutine;

        private IEnumerator StartRandomWalk()
        {
            float passedTime = 0;
            _agent.destination = _walkablePlane.GetRandomPointOnNavMesh();

            while (_agent.pathPending)
                yield return null;

            while (_agent.remainingDistance > _config.DistanceAccuracy && passedTime < _config.MaxWalkDuration)
            {
                yield return null;
                passedTime += Time.deltaTime;
            }

            Completed?.Invoke();
            Exit();
        }
    }
}