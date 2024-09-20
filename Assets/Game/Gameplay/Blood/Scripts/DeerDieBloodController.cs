using System.Collections.Generic;
using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using DoubleDCore.Service;
using Game.Gameplay.Blood.Scripts.Configs;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Blood.Scripts
{
    public class DeerDieBloodController : MonoService
    {
        [SerializeField] private Transform _bloodRoot;
        
        private DeerFabric _fabric;
        private DayCycleController _dayCycleController;
        private DeerBloodConfig _config;
        private BloodOption _bloodOption;

        private readonly List<GameObject> _bloodToClear = new();
        private readonly Dictionary<Deer, BloodAppearAnimation> _deersBlood = new();
        
        [Inject]
        private void Init(DeerFabric deerFabric, DayCycleController dayCycleController,
            IResourcesContainer resourcesContainer, BloodOption bloodOption)
        {
            _fabric = deerFabric;
            _dayCycleController = dayCycleController;
            _config = resourcesContainer.GetResource<ConfigsResource>().GetConfig<DeerBloodConfig>();
            _bloodOption = bloodOption;
        }

        private void OnEnable()
        {
            _fabric.Created += OnDeerCreated;
            _dayCycleController.DayEnded += ClearBlood;
            _bloodOption.ValueChanged += OnBloodActiveChanged;
        }

        private void OnDisable()
        {
            _fabric.Created -= OnDeerCreated;
            _dayCycleController.DayEnded -= ClearBlood;
            _bloodOption.ValueChanged -= OnBloodActiveChanged;
        }

        private void OnDeerCreated(Deer deer)
        {
            deer.Killed += OnDeerKilled;
            deer.Cutted += OnDeerCutted;
        }

        private void OnDeerKilled(Deer deer)
        {
            deer.Killed -= OnDeerKilled;
            
            Vector3 bloodPosition = deer.transform.position;
            BloodAppearAnimation bloodPrefab = Instantiate(_config.BloodDecalPrefab, _bloodRoot);
            bool isBloodActive = _bloodOption.IsActive;
            
            bloodPrefab.gameObject.SetActive(isBloodActive);
            
            if (isBloodActive)
                bloodPrefab.StartAnimation();
            
            bloodPosition.y = bloodPrefab.transform.position.y;
            bloodPrefab.transform.position = bloodPosition;
            
            _deersBlood.Add(deer, bloodPrefab);
        }

        private void OnDeerCutted(Deer deer)
        {
            deer.Cutted -= OnDeerCutted;
            
            if (_deersBlood.TryGetValue(deer, out BloodAppearAnimation blood))
            {
                _bloodToClear.Add(blood.gameObject);
                _deersBlood.Remove(deer);
            }
        }

        private void ClearBlood()
        {
            for (int i = 0; i < _bloodToClear.Count; i++)
                Destroy(_bloodToClear[i]);
            
            _bloodToClear.Clear();
        }

        private void OnBloodActiveChanged(bool isActive)
        {
            foreach (var blood in _deersBlood.Values)
                blood.gameObject.SetActive(isActive);

            foreach (var bloodToClear in _bloodToClear)
                bloodToClear.SetActive(isActive);
        }
    }
}