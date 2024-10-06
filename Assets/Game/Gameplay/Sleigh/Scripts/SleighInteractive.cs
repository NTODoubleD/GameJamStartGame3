using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Gameplay.Interaction;
using Game.Gameplay.SurvivalMeсhanics.Fatigue;
using Game.UI.Pages;
using Game.WorldMap;
using Infrastructure;
using Infrastructure.GameplayStates;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class SleighInteractive : UpgradingInteractiveObject
    {
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private CanvasGroup _whiteScreen;

        private GameplayLocalStateMachine _stateMachine;
        private WorldMapController _worldMap;
        private CursorInteractor _cursorInteractor;
        private FatigueRadialButtonsHelper _helper;
        private GameInput _gameInput;

        [Inject]
        private void Init(GameplayLocalStateMachine stateMachine, WorldMapController worldMap,
            CursorInteractor cursorInteractor, FatigueRadialButtonsHelper helper, GameInput gameInput)
        {
            _stateMachine = stateMachine;
            _worldMap = worldMap;
            _cursorInteractor = cursorInteractor;
            _helper = helper;
            _gameInput = gameInput;
        }

        public override void Interact()
        {
            var argument = GetRadialMenuArgument();

            if (_helper.IsFatigueEffectActive())
                argument.Buttons = new List<RadialButtonInfo> { _helper.GetTiredButtonInfo() };

            UIManager.OpenPage<RadialMenuPage, RadialMenuArgument>(argument);
        }

        public void OpenSortiePage()
        {
            UIManager.OpenPage<SortiePage>();
        }

        public async void ToMapState()
        {
            _gameInput.Player.Disable();

            _effect.Play();

            await UniTask.WaitForSeconds(0.5f);

            _whiteScreen.DOFade(1, 1).OnComplete(() =>
            {
                _gameInput.Player.Enable();
                _stateMachine.Enter<MapState>();
                _worldMap.Open();
                _cursorInteractor.Open();

                _effect.Stop();
                _whiteScreen.DOFade(0, 1);
            });
        }
    }
}