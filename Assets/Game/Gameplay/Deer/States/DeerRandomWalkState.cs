using System.Collections;
using DoubleDTeam.StateMachine.Base;
using Game.Gameplay.AI;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.States
{
    public class DeerRandomWalkState : IState
    {
        private const float WalkPointsInterval = 5f;

        private readonly WalkablePlane _walkablePlane;
        private readonly NavMeshAgent _agent;
        private readonly MonoBehaviour _coroutineRunner;

        public DeerRandomWalkState(NavMeshAgent agent, WalkablePlane walkablePlane, MonoBehaviour coroutineRunner)
        {
            _agent = agent;
            _walkablePlane = walkablePlane;
            _coroutineRunner = coroutineRunner;
        }

        public void Enter()
        {
            _agent.isStopped = false;
            _coroutine = _coroutineRunner.StartCoroutine(StartRandomWalk());
        }

        public void Exit()
        {
            _agent.isStopped = true;
            _coroutineRunner.StopCoroutine(_coroutine);
        }

        private Coroutine _coroutine;

        private IEnumerator StartRandomWalk()
        {
            while (true)
            {
                _agent.destination = _walkablePlane.GetRandomPointOnNavMesh();
                yield return new WaitForSeconds(WalkPointsInterval);
            }
        }
    }
}