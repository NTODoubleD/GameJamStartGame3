using System;
using System.Collections.Generic;
using System.Linq;
using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.Extensions;
using Game.Gameplay.AI;
using Game.Gameplay.Interaction;
using Game.Gameplay.States;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.Scripts
{
    public class DeerFabric : MonoModule
    {
        [SerializeField] private Deer _prefab;
        [SerializeField] private InteractiveObjectsWatcher _interactiveObjectsWatcher;
        [SerializeField] private Transform _container;

        [Space, SerializeField] private TextAsset _maleNames;
        [SerializeField] private TextAsset _femaleNames;

        private WalkablePlane _walkablePlane;

        private List<string> _deerMaleNames;
        private List<string> _deerFemaleNames;

        private readonly List<string> _usedNames = new();

        public event UnityAction<Deer> Created;

        private void Awake()
        {
            _walkablePlane = Services.SceneContext.GetModule<WalkablePlane>();

            _deerMaleNames = new List<string>(_maleNames.text.Split(",").Select(n => n.Trim()));
            _deerFemaleNames = new List<string>(_femaleNames.text.Split(",").Select(n => n.Trim()));
        }

        private void Start()
        {
            CreateDeer();
            CreateDeer();
            CreateDeer();
            CreateDeer();
            CreateDeer();
        }

        public void CreateDeer(DeerInfo deerInfo)
        {
            var inst = Instantiate(_prefab, _walkablePlane.GetRandomPointOnNavMesh(), Quaternion.identity, _container);
            _interactiveObjectsWatcher.AddObjectToWatch(inst.DeerInteractive);
            inst.Initialize<DeerRandomWalkState>(deerInfo);

            Created?.Invoke(inst);
        }

        private bool _isMale = true;

        public void CreateDeer(string deerName = null, DeerAge age = DeerAge.None, float hungeredDegree = -1f,
            GenderType gender = GenderType.None, DeerStatus deerStatus = DeerStatus.None)
        {
            var deerInfo = new DeerInfo
            {
                Age = age == DeerAge.None ? DeerAge.Young : age,
                HungerDegree = hungeredDegree < 0 ? 1 : hungeredDegree,
                Status = deerStatus == DeerStatus.None ? DeerStatus.Standard : deerStatus
            };

            if (gender == GenderType.None)
            {
                deerInfo.Gender = _isMale ? GenderType.Male : GenderType.Female;
                _isMale = !_isMale;
            }
            else
            {
                deerInfo.Gender = gender;
            }

            deerInfo.Name = deerName ?? GetRandomName(deerInfo.Gender);

            CreateDeer(deerInfo);
        }

        private string GetRandomName(GenderType gender)
        {
            if (_deerFemaleNames.Count <= 0 || _deerMaleNames.Count <= 0)
                _usedNames.Clear();

            string randomName = gender switch
            {
                GenderType.Female => _deerFemaleNames.Choose(),
                GenderType.Male => _deerMaleNames.Choose(),
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };

            if (_usedNames.Contains(randomName))
                return GetRandomName(gender);

            _usedNames.Add(randomName);

            return randomName;
        }
    }
}