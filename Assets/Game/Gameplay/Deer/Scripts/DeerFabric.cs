using System;
using System.Collections.Generic;
using System.Linq;
using DoubleDCore.Extensions;
using DoubleDCore.Service;
using DoubleDCore.TranslationTools.Base;
using DoubleDCore.TranslationTools.Data;
using Game.Gameplay.AI;
using Game.Gameplay.Interaction;
using Game.Gameplay.Scripts.Configs;
using Game.Gameplay.States;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Scripts
{
    public class DeerFabric : MonoService
    {
        [SerializeField] private DeerPrefabSelector _prefabSelector;
        [SerializeField] private InteractiveObjectsWatcher _interactiveObjectsWatcher;
        [SerializeField] private Transform _container;

        [Space, SerializeField] private Material[] _deerMaterials;

        [Space, SerializeField] private DeerNames _ru;
        [SerializeField] private DeerNames _en;

        [Header("Start Spawn Settings")] [SerializeField]
        private List<StartDeerInfo> _startDeerInfos;

        private WalkablePlane _walkablePlane;
        private DiContainer _diContainer;
        private DeerAgeConfig _ageConfig;

        private List<string> _deerMaleNames;
        private List<string> _deerFemaleNames;

        private readonly List<string> _usedNames = new();

        public event UnityAction<Deer> Created;

        [Inject]
        private void Init(WalkablePlane walkablePlane, DiContainer container, 
            ILanguageProvider languageProvider, DeerAgeConfig ageConfig)
        {
            _ageConfig = ageConfig;
            _walkablePlane = walkablePlane;

            _diContainer = container;

            var maleNames = languageProvider.GetLanguage() == LanguageType.Ru ? _ru.Male : _en.Male;
            var femaleNames = languageProvider.GetLanguage() == LanguageType.Ru ? _ru.Female : _en.Female;

            _deerMaleNames = new List<string>(maleNames.text.Split(",").Select(n => n.Trim()));
            _deerFemaleNames = new List<string>(femaleNames.text.Split(",").Select(n => n.Trim()));
        }

        private void Start()
        {
            foreach (var deerInfo in _startDeerInfos)
            {
                for (int i = 0; i < deerInfo.Count; i++)
                    CreateDeer(age: deerInfo.Age);
            }
        }

        public void CreateDeer(DeerInfo deerInfo)
        {
            var inst = _diContainer.InstantiatePrefabForComponent<Deer>(_prefabSelector.GetPrefab(deerInfo),
                _walkablePlane.GetRandomPointOnNavMesh(),
                Quaternion.identity,
                _container);

            _interactiveObjectsWatcher.AddObjectToWatch(inst.DeerInteractive);
            inst.DeerMeshing.SetMaterial(_deerMaterials[Random.Range(0, _deerMaterials.Length)]);
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
            
            deerInfo.AgeDays = _ageConfig.AgeTable[deerInfo.Age];

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

        [Serializable]
        private class DeerNames
        {
            [field: SerializeField] public TextAsset Male { get; private set; }
            [field: SerializeField] public TextAsset Female { get; private set; }
        }

        [Serializable]
        private class StartDeerInfo
        {
            [SerializeField] private DeerAge _age;
            [SerializeField] private int _count;

            public DeerAge Age => _age;
            public int Count => _count;
        }
    }
}