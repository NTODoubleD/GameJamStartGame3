using System;
using System.Collections.Generic;
using DoubleDCore.ObjectPooling;
using Game.Gameplay;
using Game.Gameplay.Scripts;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class HerdExplorer : MonoBehaviour
    {
        [SerializeField] private UIDeer _uiDeerPrefab;
        [SerializeField] private RectTransform _container;

        private readonly Pooler<UIDeer> _pool = new();
        private readonly List<UIDeer> _activeUI = new();

        public event Action ChosenChanged;

        private int _selectAmount;

        private Herd _herd;

        [Inject]
        private void Init(Herd herd)
        {
            _herd = herd;
        }

        public void Awake()
        {
            for (int i = 0; i < 100; i++)
            {
                var init = Instantiate(_uiDeerPrefab, Vector3.zero, Quaternion.identity, _container);
                init.gameObject.SetActive(false);
                _pool.Push(init);
            }
        }

        public void Reset()
        {
            foreach (var uiDeer in _activeUI)
            {
                uiDeer.Selected -= OnSelected;
                uiDeer.Deselected -= OnDeselected;

                uiDeer.gameObject.SetActive(false);
                _pool.Return(uiDeer);
            }

            _activeUI.Clear();

            foreach (var deer in _herd.CurrentHerd)
            {
                var uiDeer = _pool.Get();
                uiDeer.gameObject.SetActive(true);

                uiDeer.Initialize(deer.DeerInfo);
                _activeUI.Add(uiDeer);

                uiDeer.Selected += OnSelected;
                uiDeer.Deselected += OnDeselected;
            }

            foreach (var uiDeer in _activeUI)
            {
                uiDeer.Reset();
                uiDeer.SetActiveToggleButton(uiDeer.DeerInfo.Age == DeerAge.Adult);
            }

            _selectAmount = 0;

            ChosenChanged?.Invoke();
        }

        public void DisableAllActive()
        {
            foreach (var deerUI in _activeUI)
            {
                if (deerUI.IsChosen == false)
                    deerUI.SetActiveToggleButton(false);
            }
        }

        public void UpdateDeerAvailability()
        {
            foreach (var deerUI in _activeUI)
                deerUI.SetActiveToggleButton(deerUI.DeerInfo.Age == DeerAge.Adult);
        }

        public int GetChosenDeerAmount()
        {
            return _selectAmount;
        }

        private void OnSelected()
        {
            _selectAmount++;

            ChosenChanged?.Invoke();
        }

        private void OnDeselected()
        {
            _selectAmount--;

            ChosenChanged?.Invoke();
        }
    }
}