using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DoubleDCore.Service;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.UI.Pages;
using Infrastructure;
using Infrastructure.GameplayStates;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.WorldMap
{
    public class WorldMapController : MonoService
    {
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private CanvasGroup _whiteScreen;
        [SerializeField] private List<ResourcePoint> _resourcePoints;
        [SerializeField] private GameObject[] _objects;

        private GameplayLocalStateMachine _stateMachine;
        private EventSystem _eventSystem;
        private CursorInteractor _cursorInteractor;
        private GameInput _gameInput;
        private IUIManager _uiManager;

        public event Action Opened;

        [Inject]
        private void Init(GameplayLocalStateMachine stateMachine, EventSystemProvider eventSystemProvider,
            CursorInteractor cursorInteractor, GameInput gameInput, IUIManager uiManager)
        {
            _stateMachine = stateMachine;
            _eventSystem = eventSystemProvider.EventSystem;
            _cursorInteractor = cursorInteractor;
            _gameInput = gameInput;
            _uiManager = uiManager;
        }

        private void Start()
        {
            Close();
        }

        private void Open()
        {
            foreach (var o in _objects)
                o.SetActive(true);

            _uiManager.OpenPage<WorldInterestPointPage, InterestPointArgument>(new InterestPointArgument
            {
                Points = _resourcePoints.Select(r => r.GetPointInfo()).ToList()
            });

            Opened?.Invoke();
        }

        public void Close()
        {
            foreach (var o in _objects)
                o.SetActive(false);

            _uiManager.ClosePage<WorldInterestPointPage>();
        }

        public async void ToMapState()
        {
            _gameInput.Player.Disable();

            _effect.Play();

            await UniTask.WaitForSeconds(0.5f);

            _whiteScreen.DOFade(1, 1).OnComplete(() =>
            {
                _stateMachine.Enter<MapState>();

                Open();
                _cursorInteractor.Open();

                _effect.Stop();
                _whiteScreen.DOFade(0, 1);
            });
        }

        public async void ToPlayerState()
        {
            _gameInput.Map.Disable();
            _cursorInteractor.Close();
            _eventSystem.enabled = false;

            _effect.Play();

            await UniTask.WaitForSeconds(0.5f);

            _whiteScreen.DOFade(1, 1).OnComplete(() =>
            {
                _eventSystem.enabled = true;
                _stateMachine.Enter<PlayerMovingState>();

                Close();

                _effect.Stop();
                _whiteScreen.DOFade(0, 1);
            });
        }
    }
}