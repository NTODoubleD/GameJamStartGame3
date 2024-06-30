using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using Game.Gameplay.AI;
using Game.Gameplay.Interaction;
using Game.Gameplay.States;
using UnityEngine;

namespace Game.Gameplay.Scripts
{
    public class DeerFabric : MonoModule
    {
        [SerializeField] private Deer _prefab;
        [SerializeField] private InteractiveObjectsWatcher _interactiveObjectsWatcher;
        [SerializeField] private Transform _container;

        private WalkablePlane _walkablePlane;

        private void Awake()
        {
            _walkablePlane = Services.SceneContext.GetModule<WalkablePlane>();
        }

        private void Start()
        {
            CreateDeer(new DeerInfo
            {
                Name = "Max",
                Age = DeerAge.Adult,
                HungerDegree = 0.5f,
                Gender = GenderType.Male,
                Status = DeerStatus.Standard,
                WorldPosition = transform.position
            });
        }

        public void CreateDeer(DeerInfo deerInfo)
        {
            var inst = Instantiate(_prefab, _walkablePlane.GetRandomPointOnNavMesh(), Quaternion.identity, _container);

            _interactiveObjectsWatcher.AddObjectToWatch(inst.DeerInteractive);

            inst.Initialize<DeerRandomWalkState>(deerInfo);
        }
    }
}